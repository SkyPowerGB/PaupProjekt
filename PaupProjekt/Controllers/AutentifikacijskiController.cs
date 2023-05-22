using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Asn1.Misc;
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
        [HttpGet]
        public ActionResult Registracija()
        {

            ViewBag.novo = false;


            return View( ) ;
        }

        [HttpPost]
        public ActionResult Registracija( vlasnik v)
        {
            v.Lozinka = v.LozinkaA;

            var emailZauzet = db.vlasnikTab.Any(x => x.Email == v.Email);
            if (emailZauzet)
            {
                ModelState.AddModelError("Email", "Email je već zauzet");
            }
            if (ModelState.IsValid)
            {
                db.vlasnikTab.Add(v);
                db.SaveChanges();




            }
              
          
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