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


namespace PaupProjekt.Controllers
{
    public class RadnikController : Controller
    {
        ServisVozilaDB db = new ServisVozilaDB();

        // GET: Radnik
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult klijenti(string status) {
            var baza = db.servisTab.ToList();
            if (!String.IsNullOrWhiteSpace(status))
            {
                baza = baza.Where(x => x.StatusServisa.Contains(status)).ToList();

            }

            

            return View(baza);
        }

        public ActionResult detalji(int? id) {

            if (id == null) { return RedirectToAction("klijenti"); }


            servis Servis = db.servisTab.FirstOrDefault(x => x.ServisID == id);

            if (Servis == null) { return RedirectToAction("klijenti"); }




                    return View(Servis);
            }


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
                return HttpNotFound("Nije moguce obrisat korisnika ciji je racun vec izdan");
                var usluge = db.ListaUslugaTab.FirstOrDefault(x => x.RačunID == racunServisa.RačunID);

                if (usluge != null)
                {


                }
                else
                {


                }


            }
            else {

                db.servisTab.Remove(servisKorisnika);
                db.SaveChanges();

            }



            return RedirectToAction("klijenti");
        }


    }
}