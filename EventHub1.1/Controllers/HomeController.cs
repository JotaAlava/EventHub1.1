using System.Web.Mvc;
using EventHub1._1.Filters;

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

        [AdminAuth]
        public ActionResult Locations()
        {
            return View("Locations");
        }

        [AdminAuth]
        public ActionResult Activities()
        {
            return View("Activity");
        }

        [AdminAuth]
        public ActionResult Players()
        {
            return View("Players");
        }

        [AdminAuth]
        public ActionResult Events()
        {
            return View("Events");
        }
    }
}