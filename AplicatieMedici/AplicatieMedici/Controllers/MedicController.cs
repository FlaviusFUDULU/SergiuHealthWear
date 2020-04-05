using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
    public class MedicController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TaxePrestabiliteModel taxePrestabilite;

        public MedicController()
        {
            taxePrestabilite = db.TaxePrestabilite.FirstOrDefault();
        }

        public ActionResult _LayoutMedic()
        {
            string medicguid = User.Identity.GetUserId();

            MedicAndPacientsViewModel medicAndPacientsViewModel = new MedicAndPacientsViewModel();
            medicAndPacientsViewModel.Medic =
                db.Medic
                .Where(_ => _.accountGuid.ToString() == medicguid)
                .SingleOrDefault();

            return View(medicAndPacientsViewModel);
        }

        // GET: Salariat
        public ActionResult Index(string message, string type = "Edit", string query = "")
        {
            ViewBag.Message = (message == null) ? "" : message;
            ViewBag.Type = type;
            var dateAdministratoriList = db.DateAdministratorModels.Where(a => a.Nume.Contains(query) || a.Prenume.Contains(query)).ToList();
            return View(dateAdministratoriList);
        }

        public ActionResult ProgramariDetails()
        {
            string guid = User.Identity.GetUserId();
            Medic medic = db.Medic
                .Include(_ => _.Programari.Select(y => y.Pacient))//.OrderBy(_ => _.StartDateTime))

                .Where(_ => _.accountGuid.ToString() == guid)
                .FirstOrDefault();

            DateTime refereceDateTime = DateTime.Now;

            medic.Programari =
                medic.Programari
                .Where(_ => _.StartDateTime >= refereceDateTime)
                .OrderBy(_ => _.StartDateTime)
                .ToList();

            return View(medic.Programari);
        }

        public ActionResult Programare(string id)
        {
            string guid = User.Identity.GetUserId();
            Medic medic = db.Medic
                .Include(_ => _.Programari.Select(y => y.Pacient))//.OrderBy(_ => _.StartDateTime))

                .Where(_ => _.accountGuid.ToString() == guid)
                .FirstOrDefault();
            Pacient pacient = db.Pacient.Find(id);

            DateTime refereceDateTime = DateTime.Now;

            Programare programare = new Programare
            {
                StartDateTime = refereceDateTime,
                EndDateTime = refereceDateTime,
                Medic = medic,
                Pacient = pacient,
                MedicCNP = medic.CNP,
                PacientCNP = pacient.CNP
            };

            medic.Programari.Add(programare);
            medic.Programari =
                medic.Programari
                .Where(_ => _.StartDateTime >= refereceDateTime)
                .OrderBy(_ => _.StartDateTime)
                .ToList();

            return View(medic.Programari);
        }

        [HttpPost]
        public ActionResult Programare(IList<Programare> programari)
        {
            if (ModelState.IsValid)
            {
                //dateAdministratorModel = CalculeazaTaxe(ref dateAdministratorModel);
                Programare programare = programari.OrderBy(_ => _.Id).FirstOrDefault();

                Pacient pacient = db.Pacient.Find(programare.PacientCNP);
                Medic medic = db.Medic.Find(programare.MedicCNP);

                string emailtext = createEmail(pacient, medic, programare);

                DateTime tempStart;
                DateTime tempStop;
                tempStart = programare.StartDateTime;
                tempStop = programare.EndDateTime;
                db.Programare.Add(programare);

                programare.StartDateTime = new DateTime(tempStart.Year, tempStart.Day, tempStart.Month, tempStart.Hour, tempStart.Minute, tempStart.Second);
                programare.EndDateTime = new DateTime(tempStop.Year, tempStop.Day, tempStop.Month, tempStop.Hour, tempStop.Minute, tempStop.Second);
                //db.Entry(programari).State = EntityState.Added;
                db.SaveChanges();
                sendProgramareInEmail(pacient.Email,emailtext);
            }
            return RedirectToAction("ProgramariDetails", "Medic");
        }

        public ActionResult Consultatie()
        {
            string guid = User.Identity.GetUserId();
            Medic medic = db.Medic
                .Include(_ => _.OrarMedic)
                .Include(_ => _.Programari)
                .Where(_ => _.accountGuid.ToString() == guid)
                .FirstOrDefault();

            DateTime mondayStartHour = DateTime.ParseExact(medic.OrarMedic.LuniStart, "HH:mm",
                                        CultureInfo.InvariantCulture);
            DateTime mondayEndHour = DateTime.ParseExact(medic.OrarMedic.LuniEnd, "HH:mm",
                                                  CultureInfo.InvariantCulture);
            DateTime hour = DateTime.ParseExact(medic.OrarMedic.LuniStart, "HH:mm",
                                        CultureInfo.InvariantCulture);

            while (hour != mondayEndHour)
            {

            }

            string test = "test";

            List<string> testList = new List<string>();
            testList.Add(test);

            return View(testList);
        }

        public ActionResult Orar()
        {
            string guid = User.Identity.GetUserId();
            Medic medic = db.Medic
                .Include(_ => _.OrarMedic)
                .Where(_ => _.accountGuid.ToString() == guid)
                .FirstOrDefault();

            if (medic.OrarMedic == null)
            {
                medic.OrarMedic = new Orar();
                medic.OrarMedic.MedicCNP = medic.CNP;
            }

            return View(medic.OrarMedic);
        }

        [HttpPost]
        public ActionResult Orar(Orar orar)
        {
            if (ModelState.IsValid)
            {
                //dateAdministratorModel = CalculeazaTaxe(ref dateAdministratorModel);
                try
                {
                    db.Entry(orar).State = EntityState.Added;
                    db.SaveChanges();
                }
                catch
                {
                    db.Entry(orar).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            return View(orar);
        }

        private string createEmail(Pacient pacient, Medic medic, Programare programare)
        {
            string mesaj = $"Bună ziua {pacient.Prenume} {pacient.Nume},\n\n" +
                $"V-a fost confirmată o programare de către Doctor {medic.Prenume} {medic.Nume} pentru" +
                $" data de {programare.StartDateTime.ToString("dd/MM/yyyy")} între orele {programare.StartDateTime.ToString("HH:mm")}" +
                $" - {programare.EndDateTime.ToString("HH:mm")}. \n\n";
            
            mesaj = mesaj +
                $"Vă rugăm să contactați medicul de familie în caz de orice nelămurire la numărul de telefon: {medic.Numartelefon}\n\n" +
                $"Vă mulțumim!";
            return mesaj;
        }

        private void sendProgramareInEmail(string emailPacient, string emailBody)
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

            msg.Subject = "Programare noua";
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

        // GET: Salariat/Details/5
        public ActionResult Details(int? id)
        {
            MedicAndPacientsViewModel medicAndPacientsViewModel = new MedicAndPacientsViewModel();
            Medic medic;
            Pacient pacient = new Pacient();
            if (id == null)
            {
                string guid = User.Identity.GetUserId();
                pacient = db.Pacient
                    .Include(_ => _.Istorics)
                    .Include(_ => _.Programari)
                    .Where(_ => _.AccountGuid.ToString() == guid)
                    .FirstOrDefault();
                medic = db.Medic.Find(pacient.MedicCNP);
            }
            else
            {
                medic = db.Medic.Find(id);
            }
            if (medic == null)
            {
                return HttpNotFound();
            }
            medicAndPacientsViewModel.Medic = medic;
            medicAndPacientsViewModel.Pacient = pacient;
            return View(medicAndPacientsViewModel);
        }

        // GET: Salariat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salariat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //public ActionResult Create([Bind(Include = "CNP,Nume,Prenume,Email,Functie,Adresa,TelefonPersonal,TelefonServici")] DateAdministratorModel dateAdministratorModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DateAdministratorModels.Add(dateAdministratorModel);
        //        db.SaveChanges();

        //        createAdminUser(dateAdministratorModel.Nume, dateAdministratorModel.Prenume,
        //                        dateAdministratorModel.Email, dateAdministratorModel.CNP);

        //        return RedirectToAction("Create", new { message = "Creat cu succes! Parola este CNP-ul" });
        //    }

        //    return View(dateAdministratorModel);
        //}

        // GET: Salariat/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateAdministratorModel dateAdministratorModel = db.DateAdministratorModels.Find(id);
            if (dateAdministratorModel == null)
            {
                return HttpNotFound();
            }
            return View(dateAdministratorModel);
        }

        // POST: Salariat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CNP,Nume,Prenume,Email,Functie,Adresa,TelefonPersonal,TelefonServici")] DateAdministratorModel dateAdministratorModel)
        {
            if (ModelState.IsValid)
            {
                //dateAdministratorModel = CalculeazaTaxe(ref dateAdministratorModel);
                db.Entry(dateAdministratorModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { message = "Editat cu succes!" });
            }
            return View(dateAdministratorModel);
        }

        // GET: Salariat/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateAdministratorModel dateAdministratorModel = db.DateAdministratorModels.Find(id);
            if (dateAdministratorModel == null)
            {
                return HttpNotFound();
            }
            return View(dateAdministratorModel);
        }

        // POST: Salariat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DateAdministratorModel dateAdministratorModel = db.DateAdministratorModels.Find(id);
            db.DateAdministratorModels.Remove(dateAdministratorModel);
            db.SaveChanges();
            return RedirectToAction("Index", new { message = "Administrator ștearsă cu succes din sistem!" });
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
