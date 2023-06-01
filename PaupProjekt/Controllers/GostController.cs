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
            if(User.Identity.IsAuthenticated) { return RedirectToAction("Index","Korisnik"); }
            
            return View();
        }

        public ActionResult Usluge()
        {

            return View();
        }


        public ActionResult Kontakt() {

         
            return View();
        }
    }
}