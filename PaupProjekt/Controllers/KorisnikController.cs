using Microsoft.Ajax.Utilities;
using PaupProjekt.Misc;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class KorisnikController : Controller
    {
        // GET: Korisnik
        ServisVozilaDB baza = new ServisVozilaDB();
        [Authorize]
        public ActionResult Index(int? id)
        {
           var Email=HttpContext.User.Identity.Name;
           

           vlasnik Trazeni= baza.vlasnikTab.FirstOrDefault(x=>x.Email==Email);

         
            var narudzbe = baza.servisTab.Where(x => x.VlasnikID == Trazeni.VlasnikID).ToList();
            ViewBag.Racuni = baza.racunTab.Where(x=>x.Narudzba.VlasnikID==Trazeni.VlasnikID).ToList();


           // dsfsd
            return View(narudzbe);
        }

        [Authorize]
        public ActionResult Profil() {

            var Email = HttpContext.User.Identity.Name;
            vlasnik KorRacun = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);



            return View(KorRacun);
        }

        [Authorize]
        [HttpGet]
        public ActionResult PromjenaKorisnickihPod() {
            var Email = HttpContext.User.Identity.Name;
            vlasnik KorRacun = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);


            return View(KorRacun);
        }


        [Authorize]
        [HttpPost]
        public ActionResult PromjenaKorisnickihPod(vlasnik v)
        {

            if (ModelState.IsValid) {

                baza.Entry(v).State= System.Data.Entity.EntityState.Modified;

                baza.SaveChanges();


                return RedirectToAction("Profil");
            }


            return View(v);
        }

        public ActionResult Vozila() {

            var Email = HttpContext.User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);
            var Vozila = baza.voziloTab.Where(x=>x.voziloVlasnika.VlasnikID==Trazeni.VlasnikID).ToList();
            
            return View(Vozila);
        }
        [Authorize]
      

        public ActionResult obrisiVozilo(int? idVozila) {
     
            if (idVozila.HasValue)
            {
                ViewBag.Title = "Jeste li sigurni da želite izbrisat Vozilo ";
                vozilo v = baza.voziloTab.FirstOrDefault(x => x.VoziloId == idVozila);
                servis s = baza.servisTab.FirstOrDefault(x=>x.voziloID==idVozila);
                if (s == null)
                {
                    return View(v);
                }
            }
            ViewBag.Title = "Greška Vozilo je naručeno";
            
            return RedirectToAction("Index","Korisnik");
        }

        [HttpPost]
        public ActionResult obrisiVozilo(int id) {

            vozilo v = baza.voziloTab.FirstOrDefault(x => x.VoziloId== id);
            servis s = baza.servisTab.FirstOrDefault(x => x.voziloID == id);
            if (v == null) { return HttpNotFound("nije naden vlasnik vozila"); }
            if (s == null)
            {
                baza.voziloTab.Remove(v);
                baza.SaveChanges();
            }
            return RedirectToAction("Vozila");
        
        }
     
        [Authorize]
        public ActionResult dodajVozilo() {
           
        return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult dodajVozilo(vozilo voz)

        {
            var Email = HttpContext.User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);

            voz.VlasnikID = Trazeni.VlasnikID;



            if (ModelState.IsValid) { 
            
            
            baza.voziloTab.Add(voz);
                baza.SaveChanges();
            }

          


            return RedirectToAction("Vozila");
        }
    }
}