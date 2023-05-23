using PaupProjekt.Misc;
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
        [Authorize]
        public ActionResult Index(int? id)
        {
           var Email=HttpContext.User.Identity.Name;

           vlasnik Trazeni= db.vlasnikTab.FirstOrDefault(x=>x.Email==Email);


            var servisTrazenog = db.servisTab.FirstOrDefault(x => x.VlasnikID == Trazeni.VlasnikID);



           // dsfsd
            return View(servisTrazenog);
        }

        public ActionResult Usluge() { 
        
        return View();  
        }
        public ActionResult Profil() {
        
        return View();
        }



    }
}