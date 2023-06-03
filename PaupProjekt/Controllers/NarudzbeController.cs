using Microsoft.Ajax.Utilities;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class NarudzbeController : Controller
    {
        ServisVozilaDB baza = new ServisVozilaDB();
        // GET: Narudzbe
        servis NovaNarudzba;
      
       [Authorize]
        public ActionResult Index(string usluga)
        {
            ViewBag.UslugaPoznata = false;
            var Email = HttpContext.User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);
            var vozila = baza.voziloTab.Where(x => x.VlasnikID == Trazeni.VlasnikID).ToList();
            ViewBag.VozilaList = vozila;

            servis noviServis = new servis();
            noviServis.StatusServisa = ListaStatusaE.DopusteneVrijednosi.First();

            if (!string.IsNullOrEmpty(usluga))
            {
                ViewBag.UslugaPoznata = true;
                ViewBag.usluga = usluga;
                noviServis.OpisProblema = usluga;
            }
           
            return View(noviServis);
        }

        [HttpPost]
        public ActionResult Index(servis ser)
        {
            ViewBag.UslugaPoznata = false;
            var Email = User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);
            ser.VlasnikID = Trazeni.VlasnikID;
            ser.Datum = DateTime.Now;
            NovaNarudzba = ser;
           
/*
 * Stari kod za spremanje:
            if (ModelState.IsValid)
            {
                baza.servisTab.Add(ser);
                baza.SaveChanges();
            }


     var vozila = baza.voziloTab.Where(x => x.VlasnikID == Trazeni.VlasnikID).ToList();
            ViewBag.VozilaList = vozila;

 */

            return RedirectToAction("Potvrda",ser);
        }
        [HttpGet]
        public ActionResult Potvrda(servis ser) {

            if (ser== null) { return HttpNotFound("greska nije postavljen"); }
            ViewBag.Vozilo = baza.voziloTab.FirstOrDefault(x=>x.VoziloId==ser.voziloID);
            ViewBag.Vlasnik=baza.vlasnikTab.FirstOrDefault(x=>x.VlasnikID==ser.VlasnikID);


            return View(ser);
        }
        [HttpPost]
        public ActionResult PotvrdaNarudzbe(servis ser)
        {
            var Email = User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);

            //trerba dodat ua kontrolu ako vozilo i vlasnik id pripada korisniku koji je ulogiran
            if (ModelState.IsValid)
            {
                baza.servisTab.Add(ser);
                baza.SaveChanges();
            }
            
            var vozila = baza.voziloTab.Where(x => x.VlasnikID == Trazeni.VlasnikID).ToList();
            ViewBag.VozilaList = vozila;
            return RedirectToAction("Index", "Korisnik");
        }



        }
}