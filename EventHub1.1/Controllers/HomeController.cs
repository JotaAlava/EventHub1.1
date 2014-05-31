using EventHub1._1.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventHub1._1.Controllers
{
    [AutomaticUserCreation]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Locations()
        {
            return View("Locations");
        }

        public ActionResult Activities()
        {
            return View("Activity");
        }

        public ActionResult Players()
        {
            return View("Players");
        }

        public ActionResult Events()
        {
            return View("Events");
        }
	}
}