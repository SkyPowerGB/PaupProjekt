using Microsoft.Ajax.Utilities;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class AutentifikacijskiController : Controller
    {
        // GET: Autentifikacijski
        ServisVozilaDB db = new ServisVozilaDB();
        
        public ActionResult Registracija()
        {
            var baza=db.vlasnikTab.ToList();

            
            return View();
        }

        [HttpPost]

        public ActionResult Registracija(vlasnik v) { 
        
        return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult PromjenaLozinke(int? id)
        {
            return View();
        }
    }
}