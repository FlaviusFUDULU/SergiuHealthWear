using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AplicatieSalariati.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AplicatieSalariati.Controllers
{
    [Authorize]
    public class AngajatController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TaxePrestabiliteModel taxePrestabilite;

        public AngajatController() {
            //taxePrestabilite = db.TaxePrestabilite.FirstOrDefault();
        }

        // GET: Salariat
        public ActionResult Index(string message, string type = "Edit", string query = "")
        {
            ViewBag.Message = (message == null) ? "" : message;
            ViewBag.Type = type;
            var dateManagerList = db.DateAngajatModels.Where(a => a.CNP.Contains(query) || a.Nume.Contains(query)).ToList();
            return View(dateManagerList);
        }

        // GET: Salariat/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateAngajatModel dateAngajatModel = db.DateAngajatModels.Find(id);
            if (dateAngajatModel == null)
            {
                return HttpNotFound();
            }
            return View(dateAngajatModel);
        }

        // GET: Salariat/Create
        public ActionResult Create()
        {
            var model = new DateAngajatModel();
            model.ListEchipa = GetAllTeams();
            return View(model);
        }

        // POST: Salariat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "CNP,Nume,Prenume,Email,Functie,Echipa,Adresa,TelefonPersonal,TelefonServici")] DateAngajatModel dateAngajatModel)
        {
            if (ModelState.IsValid)
            {
                db.DateAngajatModels.Add(dateAngajatModel);
                db.SaveChanges();

                createManagerUser(dateAngajatModel.Nume, dateAngajatModel.Prenume,
                                dateAngajatModel.Email, dateAngajatModel.CNP);

                return RedirectToAction("Create", new { message = "Creat cu succes! Parola este CNP-ul" });
            }

            return View(dateAngajatModel);
        }

        // GET: Salariat/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateAngajatModel dateAngajatModel = db.DateAngajatModels.Find(id);
            dateAngajatModel.ListEchipa = GetAllTeams();
            if (dateAngajatModel == null)
            {
                return HttpNotFound();
            }
            return View(dateAngajatModel);
        }

        // POST: Salariat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CNP,Nume,Prenume,Email,Functie,Echipa,Adresa,TelefonPersonal,TelefonServici")] DateAngajatModel dateAngajatModel)
        {
            if (ModelState.IsValid)
            {
                //dateAdministratorModel = CalculeazaTaxe(ref dateAdministratorModel);
                db.Entry(dateAngajatModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { message = "Editat cu succes!" });
            }
            return View(dateAngajatModel);
        }

        // GET: Salariat/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateAngajatModel dateAngajatModel = db.DateAngajatModels.Find(id);
            if (dateAngajatModel == null)
            {
                return HttpNotFound();
            }
            return View(dateAngajatModel);
        }

        // POST: Salariat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DateAngajatModel dateAngajatModel = db.DateAngajatModels.Find(id);
            db.DateAngajatModels.Remove(dateAngajatModel);
            db.SaveChanges();
            return RedirectToAction("Index", new { message = "Administrator ștearsă cu succes din sistem!"});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void createManagerUser(string nume, string prenume, string email, string cnp)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // first we create Admin rool   
            var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            role.Name = "Angajat";
            roleManager.Create(role);

            //Here we create a Admin super user who will maintain the website                  

            var user = new ApplicationUser();
            user.UserName = prenume[0]+nume;
            user.Email = email+"@gmail.com";

            string userPWD = cnp;

            var chkUser = UserManager.Create(user, userPWD);

            //Add default User to Role Admin   
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Angajat");

            }

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("fudulu.flavius@gmail.com", "cib3rweb");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("fudulu.flavius@gmail.com");
            msg.To.Add(new MailAddress(email));

            msg.Subject = "Credentials";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><b>The password is your cnp</b></body>");

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

        public List<SelectListItem> GetAllTeams()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            //DateEchipaModel dateEchipaModel = new DateEchipaModel();
            foreach (var item in db.DateEchipeModels)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.NumeEchipa,
                    Value = item.NumeEchipa
                });
            }
            return listItems;
        }

        #region Private Helpers
        private SalariatModel CalculeazaTaxe(ref SalariatModel model) {
            var precision = 2;
            if (taxePrestabilite != null) {
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
