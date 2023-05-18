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
        
        public ActionResult Registracija()
        {
           



            return View() ;
        }

        [HttpPost]
        public ActionResult Registracija(vlasnik v)
        {
            if (!String.IsNullOrWhiteSpace(v.Email)) {
                bool racunPostoji = db.vlasnikTab.Any(x=>x.Email==v.Email);
            if(racunPostoji)
                {
                    ModelState.AddModelError("Email","email zauzet molim da se prijavite");

                }

            }
            if (ModelState.IsValid) {

                v.Lozinka = v.LozinkaA;

               
                db.vlasnikTab.Add(v);
                db.SaveChanges();

            }

            return View(v);
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