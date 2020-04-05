using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AplicatieSalariati.Models;
using AplicatieSalariati.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AplicatieSalariati.Controllers
{
    [Authorize]
    public class PacientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TaxePrestabiliteModel taxePrestabilite;


        public PacientController()
        {
            
        }

        // GET: Salariat
        public ActionResult Index(string message, string type = "Edit", string query = "")
        {
            ViewBag.Message = (message == null) ? "" : message;
            ViewBag.Type = type;
            string guid = User.Identity.GetUserId();
            Medic medic = db.Medic.Where(_ => _.accountGuid.ToString() == guid).FirstOrDefault();
            MedicAndPacientsViewModel medicAndPacientsViewModel = new MedicAndPacientsViewModel();

            medicAndPacientsViewModel.Pacienti = db.Pacient.Where(
                (a => a.Nume.Contains(query)
                || a.Prenume.Contains(query))
                ).Where(a => a.MedicCNP == medic.CNP).ToList();

            string medicguid = User.Identity.GetUserId();

            medicAndPacientsViewModel.Medic =
                db.Medic
                .Where(_ => _.accountGuid.ToString() == medicguid)
                .SingleOrDefault();

            return View(medicAndPacientsViewModel);
        }

        // GET: Salariat/Details/5
        public ActionResult Details(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacient.Find(id);

            db.Entry(pacient).Collection(p => p.Istorics).Load();

            if (pacient == null)
            {
                return HttpNotFound();
            }

            MedicAndPacientsViewModel medicAndPacientsViewModel = new MedicAndPacientsViewModel();
            medicAndPacientsViewModel.Pacient = pacient;
            string guid = User.Identity.GetUserId();
            Medic medic = db.Medic.Where(_ => _.accountGuid.ToString() == guid).FirstOrDefault();
            medicAndPacientsViewModel.Medic = medic;

            return View(medicAndPacientsViewModel);
        }

        [HttpPost]
        public ActionResult Details(
            string cnp,
            HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + cnp + ".jpeg");
                //return RedirectToAction("Details", "Pacient", new { id = id });
            }
            return RedirectToAction("Details", "Pacient", new { id = cnp });
        }

        // GET: Salariat/Create
        public ActionResult Create()
        {
            MedicAndPacientsViewModel medicAndPacientsViewModel = new MedicAndPacientsViewModel();

            string medicguid = User.Identity.GetUserId();

            medicAndPacientsViewModel.Medic =
                db.Medic
                .Where(_ => _.accountGuid.ToString() == medicguid)
                .SingleOrDefault();

            return View(medicAndPacientsViewModel);
        }

        // POST: Salariat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(MedicAndPacientsViewModel medicAndPacientsViewModel)
        {
            if (ModelState.IsValid)
            {
                string medicguid = User.Identity.GetUserId();

                medicAndPacientsViewModel.Medic =
                    db.Medic
                    .Where(_ => _.accountGuid.ToString() == medicguid)
                    .SingleOrDefault();

                medicAndPacientsViewModel.Pacient.MedicCNP = 
                    medicAndPacientsViewModel.Medic.CNP;

                string newUserguid = createPacientUser(medicAndPacientsViewModel.Pacient.Nume, 
                    medicAndPacientsViewModel.Pacient.Prenume,
                    medicAndPacientsViewModel.Pacient.Email, 
                    medicAndPacientsViewModel.Pacient.CNP);

                medicAndPacientsViewModel.Pacient.AccountGuid = Guid.Parse(newUserguid);

                db.Pacient.Add(medicAndPacientsViewModel.Pacient);
                db.SaveChanges();

                return RedirectToAction("Index", new { message = "Creat cu succes! Parola este CNP-ul" });
            }

            return View(medicAndPacientsViewModel.Pacient);
        }

        // GET: Salariat/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicAndPacientsViewModel medicAndPacientsViewModel = new MedicAndPacientsViewModel();
            Pacient pacient = db.Pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }

            string medicguid = User.Identity.GetUserId();

            medicAndPacientsViewModel.Medic =
                db.Medic
                .Where(_ => _.accountGuid.ToString() == medicguid)
                .SingleOrDefault();

            medicAndPacientsViewModel.Medic.CNP =
                medicAndPacientsViewModel.Medic.CNP;

            medicAndPacientsViewModel.Pacient = pacient;
            return View(medicAndPacientsViewModel);
        }

        // POST: Salariat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                //dateAdministratorModel = CalculeazaTaxe(ref dateAdministratorModel);
                db.Entry(pacient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { message = "Editat cu succes!" });
            }
            return View(pacient);
        }

        // GET: Salariat/Edit/5
        public ActionResult Diagnostic(string id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Istoric istoric = db.Istoric.Where(p => p.PacientCNP == id).FirstOrDefault();
            //if (istoric == null)
            //{
            //    return HttpNotFound();
            //}
            Istoric newIstoric = new Istoric()
            {
                //IstoricId = istoric.IstoricId,
                PacientCNP = id,
                Data = DateTime.Now
            };

            return View(newIstoric);
        }

        public ActionResult EditIstoric(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Istoric istoric = db.Istoric.Find(id);
            if (istoric == null)
            {
                return HttpNotFound();
            }
            //Istoric newIstoric = new Istoric()
            //{
            //    //IstoricId = istoric.IstoricId,
            //    PacientCNP = id,
            //    Data = DateTime.Now
            //};

            return View(istoric);
        }

        // POST: Salariat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Diagnostic(Istoric istoric)
        {
            if (ModelState.IsValid)
            {
                //dateAdministratorModel = CalculeazaTaxe(ref dateAdministratorModel);
                db.Entry(istoric).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index", new { message = "Editat cu succes!" });
            }
            return View(istoric);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditIstoric(Istoric istoric)
        {
            if (ModelState.IsValid)
            {
                //dateAdministratorModel = CalculeazaTaxe(ref dateAdministratorModel);
                db.Entry(istoric).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Pacient", new { id = istoric.PacientCNP });
            }
            return View(istoric);
        }

        public ActionResult DetailsReteta(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Istoric istoric = db.Istoric
                .Include(_ => _.Retete)
                .Where(_ => _.IstoricId == id)
                .FirstOrDefault();

            //List<RetetaModel> retete = new List<RetetaModel>();

            //foreach (RetetaModel reteta in istoric.Retete)
            //{
            //    rete
            //}

            return View(istoric.Retete);
        }

        public ActionResult Reteta(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RetetaModel reteta = new RetetaModel()
            {
                IstoricId = id
            };

            return View(reteta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reteta(RetetaModel reteta)
        {
            if (ModelState.IsValid)
            {
                //dateAdministratorModel = CalculeazaTaxe(ref dateAdministratorModel);
                reteta.DataEmitere = DateTime.Now;
                reteta.DataRetragere = DateTime.Now;
                db.Entry(reteta).State = EntityState.Added;
                db.SaveChanges();
                Istoric istoric = db.Istoric.Find(reteta.IstoricId);
                Pacient pacient = db.Pacient.Find(istoric.PacientCNP);
                string guid = User.Identity.GetUserId();
                Medic medic = db.Medic.Where(_ => _.accountGuid.ToString() == guid).FirstOrDefault();

                string email = createEmail(istoric, reteta, pacient, medic);

                sendRetetaInEmail(pacient.Email, email);

                return RedirectToAction("Details", "Pacient", new { id = istoric.PacientCNP });
            }
            return View(reteta);
        }
        // GET: Salariat/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Pacient pacient = db.Pacient.Find(id);
        //    if (pacient == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pacient);
        //}

        //public ActionResult DeleteReteta(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Pacient pacient = db.Pacient.Find(id);
        //    if (pacient == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pacient);
        //}

        // POST: Salariat/Delete/5
        //[HttpGet, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicAndPacientsViewModel medicAndPacientsViewModel = new MedicAndPacientsViewModel();
            Pacient pacient = db.Pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }

            string medicguid = User.Identity.GetUserId();

            medicAndPacientsViewModel.Medic =
                db.Medic
                .Where(_ => _.accountGuid.ToString() == medicguid)
                .SingleOrDefault();

            medicAndPacientsViewModel.Medic.CNP =
                medicAndPacientsViewModel.Medic.CNP;

            medicAndPacientsViewModel.Pacient = pacient;

            db.Pacient.Remove(pacient);
            db.SaveChanges();
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = await userManager.FindByEmailAsync(pacient.Email);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
  
            }
            return RedirectToAction("Index", new { message = "Pacient șters cu succes din sistem!" });
        }

        public ActionResult CalculeazaSalariu(int id)
        {
            SalariatModel salariatModel = db.Salariati.FirstOrDefault(a => a.Nr_Crt == id);
            CalculeazaTaxe(ref salariatModel);
            db.Entry(salariatModel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { type = "Calcul", message = "Recalculat cu succes!" });
        }

        public ActionResult CalculeazaToateSalariile()
        {
            var salariatiList = db.Salariati;
            foreach (var salariat in salariatiList)
            {
                var salariatModel = salariat;
                CalculeazaTaxe(ref salariatModel);
                db.Entry(salariatModel).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index", new { type = "Calcul", message = String.Format("Recalculat cu succes {0} salariați !", salariatiList.Count()) });
        }

        private string createEmail(Istoric istoric, RetetaModel reteta, Pacient pacient, Medic medic)
        {
            string mesaj = $"Bună ziua {pacient.Prenume} {pacient.Nume},\n\n" +
                $"V-a fost eliberată o rețetă de către Doctor {medic.Prenume} {medic.Nume} pentru următorul diagnostic: \n" +
                $"  -Data: {istoric.Data}.\n" +
                $"  -Diagnostic: {istoric.Diagnostic}.\n" +
                $"  -Tratament: {istoric.Tratament}\n" +
                $"  -Zile de spitalizare: {istoric.ZileSpitalizare}\n\n" +
                $"Aceasta conține:\n";
            if (reteta.Medicament1 != null)
            {
                mesaj = mesaj + $"  -{reteta.Medicament1} cu administrarea: {reteta.Administrare1}\n";
            }
            if (reteta.Medicament2 != null)
            {
                mesaj = mesaj + $"  -{reteta.Medicament2} cu administrarea: {reteta.Administrare2}\n";
            }
            if (reteta.Medicament3 != null)
            {
                mesaj = mesaj + $"  -{reteta.Medicament3} cu administrarea: {reteta.Administrare3}\n";
            }
            if (reteta.Medicament4 != null)
            {
                mesaj = mesaj + $"  -{reteta.Medicament4} cu administrarea: {reteta.Administrare4}\n";
            }
            if (reteta.Medicament5 != null)
            {
                mesaj = mesaj + $"  -{reteta.Medicament5} cu administrarea: {reteta.Administrare5}\n";
            }
            mesaj = mesaj + $"\nAceasta poate fii eliberată la orice farmacie pe baza buletinului.\n" +
                $"Vă rugăm să contactați medicul de familie în caz de orice nelămurire la numărul de telefon: {medic.Numartelefon}\n\n" +
                $"Vă mulțumim!";
            return mesaj;
        }

        private void sendRetetaInEmail(string emailPacient, string emailBody)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("retete.portal.no.reply@gmail.com", "Test123$");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("retete.portal.no.reply@gmail.com");
            msg.To.Add(new MailAddress(emailPacient));

            msg.Subject = "Reteta noua";
            msg.IsBodyHtml = false;
            msg.Body = emailBody;

            try
            {
                client.Send(msg);
                //lblMsg.Text = "Your message has been successfully sent.";
            }
            catch (Exception ex)
            {
                //lblMsg.ForeColor = Color.Red;
                //lblMsg.Text = "Error occured while sending your message." + ex.Message;
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string createPacientUser(string nume, string prenume, string email, string cnp)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // first we create Admin rool   
            //var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //role.Name = "Pacient";
            //roleManager.Create(role);

            //Here we create a Admin super user who will maintain the website                  

            var user = new ApplicationUser();
            user.UserName = email;
            user.Email = email;

            string userPWD = cnp;

            var chkUser = UserManager.Create(user, userPWD);

            //Add default User to Role Admin   
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Pacient");

            }

            return user.Id;
            //SmtpClient client = new SmtpClient();
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            //client.Host = "smtp.gmail.com";
            //client.Port = 587;

            //// setup Smtp authentication
            //System.Net.NetworkCredential credentials =
            //    new System.Net.NetworkCredential("fudulu.flavius@gmail.com", "cib3rweb");
            //client.UseDefaultCredentials = false;
            //client.Credentials = credentials;

            //MailMessage msg = new MailMessage();
            //msg.From = new MailAddress("fudulu.flavius@gmail.com");
            //msg.To.Add(new MailAddress(email));

            //msg.Subject = "Credentials";
            //msg.IsBodyHtml = true;
            //msg.Body = string.Format("<html><head></head><body><b>The password is your cnp</b></body>");

            //try
            //{
            //    client.Send(msg);
            //    //lblMsg.Text = "Your message has been successfully sent.";
            //}
            //catch (Exception ex)
            //{
            //    //lblMsg.ForeColor = Color.Red;
            //    //lblMsg.Text = "Error occured while sending your message." + ex.Message;
            //}
        }

        #region Private Helpers
        private SalariatModel CalculeazaTaxe(ref SalariatModel model)
        {
            var precision = 2;
            if (taxePrestabilite != null)
            {
                model.Total_Brut = Math.Round(((model.Salar_Brut * model.Salar_Realizat / 100) * (1 + model.Vechime / 100 + model.Spor / 100) + model.Premii_Brute + model.Compensatie), precision);
                model.CAS = Math.Round((model.Total_Brut * taxePrestabilite.CAS), precision);
                model.Somaj = Math.Round((model.Total_Brut * taxePrestabilite.Somaj), precision);
                model.Sanatate = Math.Round((model.Total_Brut * taxePrestabilite.Sanatate), precision);
                model.Brut_Impozabil = Math.Round((model.Total_Brut - model.CAS - model.Somaj - model.Sanatate), precision);
                model.Impozit = Math.Round((model.Brut_Impozabil * taxePrestabilite.Impozit), precision);
                model.RestPlata = Math.Round((model.Total_Brut - model.Impozit - model.CAS - model.Somaj - model.Sanatate - model.Retineri - model.Avans), precision);
            }
            return model;
        }
        #endregion
    }


}