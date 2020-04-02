using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CustomActionFilter.Controllers.Utils;

namespace CustomActionFilter.Controllers
{
    public class HomeController : Controller
    {
        // Decorador personalizado
        [TrackExecutionTimeController]
        public ActionResult Index()
        {            

            return View();
        }

        // Decorador personalizado
        [TrackExecutionTimeController]
        public string Exception()
        {
            throw new Exception("Exception occurred");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}