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
    public class EchipaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TaxePrestabiliteModel taxePrestabilite;

        public EchipaController() {
            //taxePrestabilite = db.TaxePrestabilite.FirstOrDefault();
        }

        // GET: Salariat
        public ActionResult Index(string message, string type = "Edit", string query = "")
        {
            ViewBag.Message = (message == null) ? "" : message;
            ViewBag.Type = type;
            var dateEchipeList = db.DateEchipeModels.Where(a => a.NumeEchipa.Contains(query) || a.NumeManager.Contains(query)).ToList();
            return View(dateEchipeList);
        }

        // GET: Salariat/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateEchipaModel echipeModel = db.DateEchipeModels.Find(id);
            if (echipeModel == null)
            {
                return HttpNotFound();
            }
            return View(echipeModel);
        }

        // GET: Salariat/Create
        public ActionResult Create()
        {
            var model = new DateEchipaModel();
            model.ListNume = GetAllManagers();
            return View(model);
        }

        // POST: Salariat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "NumeEchipa,NumeManager,NrTelManager,EmailEchipa,DomeniulDeFunctionare,Adresa")] DateEchipaModel dateEchipeModel)
        {
            if (ModelState.IsValid)
            {
                db.DateEchipeModels.Add(dateEchipeModel);
                db.SaveChanges();

                return RedirectToAction("Create", new { message = "Creat cu succes!" });
            }

            return View(dateEchipeModel);
        }

        // GET: Salariat/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateEchipaModel dateEchipeModel = db.DateEchipeModels.Find(id);
            if (dateEchipeModel == null)
            {
                return HttpNotFound();
            }
            return View(dateEchipeModel);
        }

        // POST: Salariat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NumeEchipa,NumeManager,NrTelManager,EmailEchipa,DomeniulDeFunctionare,Adresa")] DateEchipaModel dateEchipeModel)
        {
            if (ModelState.IsValid)
            {
                //dateAdministratorModel = CalculeazaTaxe(ref dateAdministratorModel);
                db.Entry(dateEchipeModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { message = "Editat cu succes!" });
            }
            return View(dateEchipeModel);
        }

        // GET: Salariat/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateEchipaModel dateEchipeModel = db.DateEchipeModels.Find(id);
            if (dateEchipeModel == null)
            {
                return HttpNotFound();
            }
            return View(dateEchipeModel);
        }

        // POST: Salariat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DateEchipaModel dateEchipeModel = db.DateEchipeModels.Find(id);
            db.DateEchipeModels.Remove(dateEchipeModel);
            db.SaveChanges();
            return RedirectToAction("Index", new { message = "Echipa ștearsă cu succes din sistem!"});
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

        public List<SelectListItem> GetAllManagers()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            //DateEchipaModel dateEchipaModel = new DateEchipaModel();
            foreach (var item in db.DateManagerModels)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.Nume,
                    Value = item.Nume
                });
            }
            return listItems;
        }
        #endregion
    }



    
}
