using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class RadnikController : Controller
    {
        // GET: Radnik
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult klijenti() {

            return View();
        }


    }
}