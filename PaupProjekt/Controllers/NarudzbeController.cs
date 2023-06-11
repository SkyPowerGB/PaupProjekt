using Microsoft.Ajax.Utilities;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace PaupProjekt.Controllers
{
    public class NarudzbeController : Controller
    {
        ServisVozilaDB baza = new ServisVozilaDB();
        // GET: Narudzbe
        servis NovaNarudzba;

        //Ulsuge Stranica (sve za narucivanje)--------------------
       

        // -------------Kreiranje novoga Servisa tj nova narudžba-----------
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

            noviServis.Datum = DateTime.Now;
            return View(noviServis);
        }

     //uzmi podatke o narudžbi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(servis ser)
        {
           
            ViewBag.UslugaPoznata = false;
            var Email = User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);
            ser.VlasnikID = Trazeni.VlasnikID;
            ser.Datum = DateTime.Now;
            NovaNarudzba = ser;

            
            if (ser.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(ser.ImageFile.FileName);
                string extension = Path.GetExtension(ser.ImageFile.FileName);

                if (extension == ".jpg" || extension == ".jepg" || extension == ".png")
                {
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    ser.slikaVozila = "~/SlikeServisi/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/SlikeServisi/"), fileName);
                    ser.ImageFile.SaveAs(fileName);

                }
                else
                {
                    ModelState.AddModelError("Slika kvara", "Nepodržana ekstenzija");
                    return View(ser);
                }

            }

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

            return RedirectToAction("Potvrda", ser);
        }
        [HttpGet]    
        //------------------Potvrdi narudžbu-------------------------
        public ActionResult Potvrda(servis ser)
        {

            if (ser == null) { return HttpNotFound("greska nije postavljen"); }
            ViewBag.Vozilo = baza.voziloTab.FirstOrDefault(x => x.VoziloId == ser.voziloID);
            ViewBag.Vlasnik = baza.vlasnikTab.FirstOrDefault(x => x.VlasnikID == ser.VlasnikID);


            return View(ser);
        }
        [HttpPost]
        //spremi 
        [ValidateAntiForgeryToken]
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
            else { return RedirectToAction("Index",ser); }

            var vozila = baza.voziloTab.Where(x => x.VlasnikID == Trazeni.VlasnikID).ToList();
            ViewBag.VozilaList = vozila;
            return RedirectToAction("Index", "Korisnik");
        }

        //---------------Odustani i (izbiriši sliku) nazad na novu narudžbu-----------------------------
      
        public ActionResult odustani(string path, string usluga)
        {
           

            if (!String.IsNullOrWhiteSpace(path))
            {
                var putanja = Server.MapPath(path);
                if (System.IO.File.Exists(putanja))
                {
                    System.IO.File.Delete(putanja);
                   
                }
                else { return HttpNotFound("slika neje nađena"); }
            }
           

            return RedirectToAction("Index", "Narudzbe", new { usluga = usluga });
        }


       
    }
}