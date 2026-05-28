using Microsoft.Ajax.Utilities;
using PaupProjekt.Misc;
using PaupProjekt.Models;
using Rotativa;
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
//--------Početna KORISNIK----------------------------------------

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
        [HttpGet]

 //--------otkazivanje narudžbe---------------------------------------
        public ActionResult OtkaziNarudzbu(int idNarudzbe) {

          
            var Narudzba = baza.servisTab.FirstOrDefault(X=>X.ServisID==idNarudzbe);


        return View(Narudzba);
        }
        [Authorize]
 //potvrdi otkazivanje
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OtkaziNarudzbu(servis Narudzba)
        {
            Narudzba.StatusServisa = "Otkazan";

            if (ModelState.IsValid) { 
            baza.Entry(Narudzba).State= System.Data.Entity.EntityState.Modified;
                baza.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Narudzba);
        }

  //--------Stranica Profila---------------------------------------
        [Authorize]
        public ActionResult Profil() {

            var Email = HttpContext.User.Identity.Name;
            vlasnik KorRacun = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);



            return View(KorRacun);
        }


//----------Promjena Podataka na profilnoj-----------------------
        [Authorize]
        [HttpGet]
        public ActionResult PromjenaKorisnickihPod() {
            

            var Email = HttpContext.User.Identity.Name;
            vlasnik KorRacun = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);


            return View(KorRacun);
        }
//preuzmi podatke i spremi
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PromjenaKorisnickihPod(vlasnik v)
        {
          
            if (ModelState.IsValid) {

                baza.Entry(v).State= System.Data.Entity.EntityState.Modified;

                baza.SaveChanges();


                return RedirectToAction("Profil");
            }


            return View(v);
        }


   //--------------Tablica Vozila Korisnika--------------------------------------------------
        [Authorize]
        public ActionResult Vozila() {

            var Email = HttpContext.User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);
            var Vozila = baza.voziloTab.Where(x=>x.voziloVlasnika.VlasnikID==Trazeni.VlasnikID).ToList();
            
            return View(Vozila);
        }
        [Authorize]

        //-----------Brisanje Vozila -------------------------------------------------------------
        [Authorize]
        //potvrdi brisanje
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
        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult obrisiVozilo(int id) {

            vozilo v = baza.voziloTab.FirstOrDefault(x => x.VoziloId== id);
            servis s = baza.servisTab.FirstOrDefault(x => x.voziloID == id);
            if (v == null) { return HttpNotFound("nije naden vlasnik vozila"); }
            if (s == null)
            {
                if (v.voziloVlasnika.Email != User.Identity.Name) { return RedirectToAction("Vozila"); }
                baza.voziloTab.Remove(v);
                baza.SaveChanges();
            }
            return RedirectToAction("Vozila");
        
        }
     
//-------------Kreiranje novoga vozila----------------------------------------------------
        [Authorize]
        public ActionResult dodajVozilo() {
           
        return View( new vozilo());
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
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

//-------------Pregled Računa prije ispisa (View ovog kontrolera ce se ispisat)----------------------
        [Authorize]
        public ActionResult pregledRacuna(int? servisId) {
            if (servisId == null) { return RedirectToAction("Index"); }
            var Racun = baza.racunTab.FirstOrDefault(x=>x.ServisID== servisId);
            if(Racun == null) { return RedirectToAction("index"); }
            ViewData["ListaUsluga"] = baza.ListaUslugaTab.Where(x => x.RačunID == Racun.RačunID).ToList();
            var Servis = baza.servisTab.FirstOrDefault(x => x.ServisID == servisId);
            if(Servis==null) { return RedirectToAction("Index"); }

            ViewData["Servis"] = Servis;
          
          
          

        return View(Racun);
        }
        [Authorize]

  //ispis
        public ActionResult ispisRacunaPDF(račun Racun )
        {
           
            if (Racun == null) { return HttpNotFound(); }
            var servis = baza.servisTab.FirstOrDefault(x=>x.ServisID==Racun.ServisID);
            if(servis == null) { return HttpNotFound(); }
            if(servis.VlasnikVozila.Email!=User.Identity.Name) { return RedirectToAction("Index"); }
            ViewData["ListaUsluga"] = baza.ListaUslugaTab.Where(x => x.RačunID == Racun.RačunID).ToList();
            ViewData["Servis"] = baza.servisTab.FirstOrDefault(x => x.ServisID == Racun.ServisID);

            return new ViewAsPdf("pregledRacuna", Racun)
            {
                FileName = "RacunServisa.pdf" 
            };
        }




    }

}