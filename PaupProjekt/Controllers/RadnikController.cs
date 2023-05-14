using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Crypto.Tls;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult klijenti() {

            var baza = db.servisTab.ToList();

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
        public ActionResult Azuriraj(servis s) {
          

            if (ModelState.IsValid)
            {
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                RedirectToAction("klijenti");

            }

            return View(s);
            
           

        }









    }
}