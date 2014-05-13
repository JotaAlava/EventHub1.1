using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventHub1._1.Controllers
{
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
	}
}