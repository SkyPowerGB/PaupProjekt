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
     
     
      //-------------------servis talblica--------------------------------------------------------------------------------------
        public ActionResult klijenti(string prezimeIme, string marka , string usluga, string status) {
            var narudzbe = db.servisTab.ToList();

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

            

            return View(narudzbe);
        }

        //----------detalji servisa-------------------------------------------------------------------------------------------
        public ActionResult detalji(int? id) {

            if (id == null) { return RedirectToAction("klijenti"); }


            servis Servis = db.servisTab.FirstOrDefault(x => x.ServisID == id);

            if (Servis == null) { return RedirectToAction("klijenti"); }




                    return View(Servis);
            }


        //----------------- Ažuriranje servisa--------------------------------------------------------------------------------------
        public ActionResult Azuriraj(int? id)
        {

            
            servis nalozi = null;
            if (!id.HasValue ) {
                RedirectToAction("klijenti");
               
            }
          
            nalozi= db.servisTab.FirstOrDefault(x=>x.ServisID==id);
            if(nalozi == null) { RedirectToAction("klijenti"); }


            return View(nalozi);
        }
       

            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj( servis Servis) {
            
            if (ModelState.IsValid)
            {

                db.Entry(Servis).State =System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                RedirectToAction("klijenti");
            }
            

            return View(Servis);
            
           

        }



        //----------------Brisanje Servisa-----------------------------------------------------------------------------

        public ActionResult ObrisiNarudzbu(int? id) {
            if (!id.HasValue) { return HttpNotFound(); }




            var servisKorisnika = db.servisTab.FirstOrDefault(x => x.ServisID == id);

            ViewBag.Title = "Jeste li sigurni da želite izbrisati narudžbu korisnika ";

            return View(servisKorisnika);
        }



        [HttpPost]
        public ActionResult ObrisiNarudzbu(int id)
        {
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