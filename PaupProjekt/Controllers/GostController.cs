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

        public ActionResult Kontakt()
        {
            var baza = db.korisnici.ToList();
            ViewBag.ime=
            baza[0].korIme;
            return View();
        }
    }
}