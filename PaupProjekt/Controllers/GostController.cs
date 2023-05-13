using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class GostController : Controller
    { 
        ServisVozilaDB db =new ServisVozilaDB();
        // GET: Gost
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Usluge()
        {

            return View();
        }


        public ActionResult Kontakt() {

            var servis = db.servisTab.ToList();
            var ovlasti = db.ovlastiTab.ToList();
            var vozila = db.voziloTab.ToList();
            var vlasniki = db.vlasnikTab.ToList();
            var računi = db.racunTab.ToList();


            ViewBag.ime = računi.Count();
            return View();
        }
    }
}