using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicatieSalariati.Models;
using System.Web.Routing;

namespace AplicatieSalariati.Controllers
{
    public class TaxePrestabiliteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaxePrestabilite
        public ActionResult Index()
        {
            return View(db.TaxePrestabilite.ToList());
        }

        // GET: TaxePrestabilite/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxePrestabiliteModel taxePrestabiliteModel = db.TaxePrestabilite.Find(id);
            if (taxePrestabiliteModel == null)
            {
                return HttpNotFound();
            }
            return View(taxePrestabiliteModel);
        }

        // GET: TaxePrestabilite/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaxePrestabilite/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Impozit,CAS,Somaj,Sanatate")] TaxePrestabiliteModel taxePrestabiliteModel)
        {
            if (ModelState.IsValid)
            {
                db.TaxePrestabilite.Add(taxePrestabiliteModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taxePrestabiliteModel);
        }

        // GET: TaxePrestabilite/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TaxePrestabiliteModel taxePrestabiliteModel = db.TaxePrestabilite.Find(id);
                if (taxePrestabiliteModel == null)
                {
                    return HttpNotFound();
                }
                return View(taxePrestabiliteModel);
            }
            else return new RedirectToRouteResult(new RouteValueDictionary {
                { "controller", "Account"},
                { "action", "Login"}
            });
        }

        // POST: TaxePrestabilite/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Impozit,CAS,Somaj,Sanatate")] TaxePrestabiliteModel taxePrestabiliteModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxePrestabiliteModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(taxePrestabiliteModel);
        }

        // GET: TaxePrestabilite/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxePrestabiliteModel taxePrestabiliteModel = db.TaxePrestabilite.Find(id);
            if (taxePrestabiliteModel == null)
            {
                return HttpNotFound();
            }
            return View(taxePrestabiliteModel);
        }

        // POST: TaxePrestabilite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxePrestabiliteModel taxePrestabiliteModel = db.TaxePrestabilite.Find(id);
            db.TaxePrestabilite.Remove(taxePrestabiliteModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
