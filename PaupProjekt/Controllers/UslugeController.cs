using Antlr.Runtime.Tree;
using PaupProjekt.Misc;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class UslugeController : Controller
    {
        // GET: Usluge
     
        ServisVozilaDB baza = new ServisVozilaDB();

        //---------Tablica Usluga------------------------------
        [Authorize]
        public ActionResult ListaUsluga(string naziv)
        {
            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }

            var usluge =  baza.uslugeTab.ToList();
            if (!String.IsNullOrWhiteSpace(naziv)) {

             usluge=   usluge.Where(x => x.nazivUsluga.ToUpper().Contains(naziv.ToUpper())).ToList();
            
            }

            return View(usluge);
        }

        //---------Uredi Uslugu------------------------------
       
        [HttpGet]
        [Authorize]

        public ActionResult UrediUslugu(int? id) {

            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }

            if (id.HasValue) {
                var usluga = baza.uslugeTab.FirstOrDefault(x=>x.UslugaID==id);
                if (usluga != null) { 
                   
                    return View(usluga);

                }
            
            
            }
        
        return View();  
        }
     
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UrediUslugu(uslugeTab usluga) {

            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }

            if (ModelState.IsValid) {

                baza.Entry(usluga).State = System.Data.Entity.EntityState.Modified;
                baza.SaveChanges();


            }
            return RedirectToAction("ListaUsluga");
        }

        //---------Obriši Uslugu----------------------------------
        
        [HttpGet]
        [Authorize]
       
        public ActionResult ObrisiUslugu(int? idUsluge)
        {
            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }

            if (!idUsluge.HasValue) { return HttpNotFound("Id nije naden"); }

            var usluga = baza.uslugeTab.FirstOrDefault(x=>x.UslugaID==idUsluge);

            ViewBag.Title = "Jeste li sigurni da zelite obrisati uslugu";
            if (usluga == null) { return RedirectToAction("ListaUsluga"); }

            return View(usluga);
        }
      
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ObrisiUslugu(int id) {
            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }

            var usluga= baza.uslugeTab.FirstOrDefault(x=>x.UslugaID == id);
            if (baza.ListaUslugaTab.FirstOrDefault(x => x.UslugaID == usluga.UslugaID) != null)
            {
                return RedirectToAction("ListaUsluga"); }
            
            baza.uslugeTab.Remove(usluga);
            baza.SaveChanges();

            return RedirectToAction("ListaUsluga");
        }

        //---------Kreiraj novu Uslugu------------------------------
        public ActionResult DodajUslugu() {
            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }


            return View();
        }
       
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DodajUslugu(uslugeTab Usluga)
         
        {

            //podaci o kornisniku
            LogiraniKorisnik kor = User as LogiraniKorisnik;
            //ak je ne radnik ->na početnu
            if (!((kor.IsInRole(OvlastiKorisnik.Radnik) || kor.IsInRole(OvlastiKorisnik.Admin))))
            {
                return RedirectToAction("Index", "Korisnik");
            }

            if (ModelState.IsValid)
            {
              
                baza.uslugeTab.Add(Usluga);
                baza.SaveChanges();


            }


            return RedirectToAction("ListaUsluga");
        }


    }



}