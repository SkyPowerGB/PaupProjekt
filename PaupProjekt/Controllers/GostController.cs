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

//početna opcenito o nama----------------------
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated) { return RedirectToAction("Index","Korisnik"); }
            
            return View();
        }
//----za Logirane: Narucivanje ---za Goste linkovi su za prijavu----------------------
        public ActionResult Usluge()
        {

            return View();
        }

//-------------Podaci za Kontakt----------------------------
        public ActionResult Kontakt() {

         
            return View();
        }
    }
}