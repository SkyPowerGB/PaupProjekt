using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class KorisnikController : Controller
    {
        // GET: Korisnik
        ServisVozilaDB db = new ServisVozilaDB();

        public ActionResult Index(int? id)
        {
            var baza = db.servisTab.ToList();
           // dsfsd
            return View(baza);
        }

        public ActionResult Usluge() { 
        
        return View();  
        }
        public ActionResult Profil() {
        
        return View();
        }



    }
}