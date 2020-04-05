using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AplicatieSalariati.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("Farmacist"))
                {
                    return RedirectToAction("Home", "Farmacist");
                }
                else if (User.IsInRole("Medic"))
                {
                    return RedirectToAction("Index", "Pacient");
                }
                else if (User.IsInRole("Pacient"))
                {
                    return RedirectToAction("Details", "Medic");
                }
                return View();
            }
            else
            {
                return View("~/Views/Account/Login.cshtml");
            }
        }

        public ActionResult About()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Message = "Your application description page.";

                return View();

            }
            else return new RedirectToRouteResult(new RouteValueDictionary {
                { "controller", "Account"},
                { "action", "Login"}
            });
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}