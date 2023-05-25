using Microsoft.Ajax.Utilities;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class NarudzbeController : Controller
    {
        ServisVozilaDB baza = new ServisVozilaDB();
        // GET: Narudzbe


        [Authorize]
        public ActionResult Index(string usluga)
        {
            var Email = HttpContext.User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);
            var vozila = baza.voziloTab.Where(x => x.VlasnikID == Trazeni.VlasnikID).ToList();
            ViewBag.VozilaList = vozila;

            servis noviServis = new servis();
            noviServis.StatusServisa = ListaStatusaE.DopusteneVrijednosi.First();

            if (!string.IsNullOrEmpty(usluga))
            {
                ViewBag.usluga = usluga;
                noviServis.OpisProblema = usluga;
            }
      
            return View(noviServis);
        }

        [HttpPost]
        public ActionResult Index(servis ser)
        {
            
            var Email = User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);
            ser.VlasnikID = Trazeni.VlasnikID;
            ser.Datum = DateTime.Now;
          

            if (ModelState.IsValid)
            {
                baza.servisTab.Add(ser);
                baza.SaveChanges();
            }

            var vozila = baza.voziloTab.Where(x => x.VlasnikID == Trazeni.VlasnikID).ToList();
            ViewBag.VozilaList = vozila;

            return RedirectToAction("Index","Korisnik");
        }



    }
}