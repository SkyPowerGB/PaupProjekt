using Antlr.Runtime.Tree;
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
        public ActionResult ListaUsluga(string naziv)
        {
      var usluge =  baza.uslugeTab.ToList();
            if (!String.IsNullOrWhiteSpace(naziv)) {

             usluge=   usluge.Where(x => x.nazivUsluga.ToUpper().Contains(naziv.ToUpper())).ToList();
            
            }

            return View(usluge);
        }

        //---------Uredi Uslugu------------------------------

        [HttpGet]
        public ActionResult UrediUslugu(int? id) {

            if (id.HasValue) {
                var usluga = baza.uslugeTab.FirstOrDefault(x=>x.UslugaID==id);
                if (usluga != null) { 
                   
                    return View(usluga);

                }
            
            
            }
        
        return View();  
        }
        [HttpPost]
        public ActionResult UrediUslugu(uslugeTab usluga) {

            if (ModelState.IsValid) {

                baza.Entry(usluga).State = System.Data.Entity.EntityState.Modified;
                baza.SaveChanges();


            }
            return RedirectToAction("ListaUsluga");
        }

        //---------Obriši Uslugu----------------------------------
        [HttpGet]
        public ActionResult ObrisiUslugu(int? idUsluge)
        {
            if (!idUsluge.HasValue) { return HttpNotFound("Id nije naden"); }

            var usluga = baza.uslugeTab.FirstOrDefault(x=>x.UslugaID==idUsluge);

            ViewBag.Title = "Jeste li sigurni da zelite obrisati uslugu";
            if (usluga == null) { return RedirectToAction("ListaUsluga"); }

            return View(usluga);
        }

        [HttpPost]
        public ActionResult ObrisiUslugu(int id) {
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

            

            return View();
        }

        [HttpPost]
        public ActionResult DodajUslugu(uslugeTab Usluga)
         
        {
      
            if(ModelState.IsValid)
            {
              
                baza.uslugeTab.Add(Usluga);
                baza.SaveChanges();


            }


            return RedirectToAction("ListaUsluga");
        }


    }



}