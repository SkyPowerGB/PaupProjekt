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
        public ActionResult Index(servis ser,HttpPostedFileBase ImageFile) // Dodaj parametar ovdje
    {
            var Email = User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);

            ser.VlasnikID = Trazeni.VlasnikID;
            ser.Datum = DateTime.Now;

            if(ImageFile != null && ImageFile.ContentLength > 0) {
                string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName) + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(ImageFile.FileName);
                string path = Path.Combine(Server.MapPath("~/SlikeServisi/"),fileName);
                ImageFile.SaveAs(path);
                ser.slikaVozila = "~/SlikeServisi/" + fileName;
            }

            // SPREMI U TEMPDATA (ovo prenosi objekt sigurno)
            TempData["Narudzba"] = ser;
            return RedirectToAction("Potvrda");
        }

        [HttpGet]
        public ActionResult Potvrda() {
            var ser = TempData["Narudzba"] as servis;
            if(ser == null)
                return RedirectToAction("Index");

            ViewBag.Vozilo = baza.voziloTab.FirstOrDefault(x => x.VoziloId == ser.voziloID);
            ViewBag.Vlasnik = baza.vlasnikTab.FirstOrDefault(x => x.VlasnikID == ser.VlasnikID);

            // Vrati u TempData da bude dostupno za idući korak (PotvrdaNarudzbe)
            TempData["Narudzba"] = ser;
            return View(ser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PotvrdaNarudzbe() {
            var ser = TempData["Narudzba"] as servis;
            if(ser == null)
                return RedirectToAction("Index");

            // Ovdje sada sigurno imaš popunjen objekt 'ser'
            baza.servisTab.Add(ser);
            baza.SaveChanges();

            return RedirectToAction("Index","Korisnik");
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