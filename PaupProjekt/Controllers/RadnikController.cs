using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Crypto.Tls;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using PaupProjekt.Misc;
using System.IO;
using System.Security.Principal;

namespace PaupProjekt.Controllers
{
    public class RadnikController : Controller
    {
     
        //--------baza----------------------------
        ServisVozilaDB db = new ServisVozilaDB();
        //-----------------------------------------

        [Authorize]
        //-------------------servis talblica--------------------------------------------------------------------------------------
        public ActionResult klijenti(string prezimeIme, string marka, string usluga, string status) {
            var narudzbe = db.servisTab.ToList();

            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin)))){
                return RedirectToAction("Index","Korisnik");
            }

            if (!String.IsNullOrWhiteSpace(usluga))
            {
                narudzbe = narudzbe.Where(x => x.OpisProblema.ToUpper().Contains(usluga.ToUpper())).ToList();
            }


            if (!String.IsNullOrWhiteSpace(prezimeIme))
            {
                narudzbe = narudzbe.Where(x => x.VlasnikVozila.PrezimeIme.ToUpper().Contains(prezimeIme.ToUpper())).ToList();
            }



            if (!String.IsNullOrWhiteSpace(status))
            {
                narudzbe = narudzbe.Where(x => x.StatusServisa.Contains(status)).ToList();

            }

            ViewBag.racuni=db.racunTab.ToList();

            return View(narudzbe);
        }

        //----------detalji servisa-------------------------------------------------------------------------------------------

        [Authorize]
       
        public ActionResult detalji(int? id) {
            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }

            if (id == null) { return RedirectToAction("klijenti"); }


            servis Servis = db.servisTab.FirstOrDefault(x => x.ServisID == id);

            if (Servis == null) { return RedirectToAction("klijenti"); }




                    return View(Servis);
            }

        [Authorize]
    
        //----------------- Ažuriranje servisa--------------------------------------------------------------------------------------
        public ActionResult Azuriraj(int? id)
        {
            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }

            servis nalozi = null;
            if (!id.HasValue ) {
                return RedirectToAction("klijenti");
               
            }
          
            nalozi= db.servisTab.FirstOrDefault(x=>x.ServisID==id);
            if(nalozi == null) { RedirectToAction("klijenti"); }


            return View(nalozi);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj( servis Servis) {

            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }

            if (ModelState.IsValid)
            {

                db.Entry(Servis).State =System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                RedirectToAction("klijenti");
            }
            

            return View(Servis);
            
           

        }



        //----------------Brisanje Servisa-----------------------------------------------------------------------------
        [Authorize]
        
        public ActionResult ObrisiNarudzbu(int? id) {
            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }

            if (!id.HasValue) { return HttpNotFound(); }


            var servisKorisnika = db.servisTab.FirstOrDefault(x => x.ServisID == id);

            ViewBag.Title = "Jeste li sigurni da želite izbrisati narudžbu korisnika ";

            return View(servisKorisnika);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ObrisiNarudzbu(int id)
        {
            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }


            var servisKorisnika = db.servisTab.FirstOrDefault(x => x.ServisID == id);

            var racunServisa = db.racunTab.FirstOrDefault(x => x.ServisID == id);

            if (racunServisa != null)
            {
                //tu treba odlucit kaj bude se desilo ak korisnik ima veci izdan racun
                if ((User as LogiraniKorisnik).IsInRole(OvlastiKorisnik.Admin)) { }
                else
                {
                    if (racunServisa.Izdan) { return RedirectToAction("klijenti"); }
                }
                var path = servisKorisnika.slikaVozila;
                   
                var putanja = Server.MapPath(path);
                if (System.IO.File.Exists(putanja))
                {
                    System.IO.File.Delete(putanja);

                }
                else {  }

                var uslugeServisa = db.ListaUslugaTab.Where(x=>x.RačunID==racunServisa.RačunID).ToList();
                if (uslugeServisa != null) { foreach (ListaUslugaTab usluga in uslugeServisa) {
                        db.ListaUslugaTab.Remove(usluga);
                    
                    
                    } }
                db.racunTab.Remove(racunServisa);
                db.SaveChanges();

            }
         

                db.servisTab.Remove(servisKorisnika);
                db.SaveChanges();

          



            return RedirectToAction("klijenti");
        }



    }
}