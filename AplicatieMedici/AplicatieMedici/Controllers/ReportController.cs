using AplicatieSalariati.Models;
using AplicatieSalariati.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicatieSalariati.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Report
        public ActionResult Index()
        {
            StatPlataViewModel statPlata = new StatPlataViewModel();
            statPlata.Salariati = db.Salariati.ToList();
            return View(statPlata);
        }

        public ActionResult Fluturasi() {
            return View(db.Salariati.ToList());
        }
    }
}