using Antlr.Runtime.Tree;
using PagedList;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace PaupProjekt.Controllers
{
    public class AdminController : Controller
    {
        ServisVozilaDB baza = new ServisVozilaDB();

       
      //--------(Tablica) --Lista Korisnika-*------------------
        public ActionResult Korisnici(int? i)
        {
            var pageNumber = i ?? 1;
            var pageSize = 10;
            var kor = baza.vlasnikTab.ToList();

            var pagedKorisnici = kor.ToPagedList(pageNumber, pageSize);

            return View(pagedKorisnici);
        }


//-----promjena ovlasti / lozinke ------------------------------
        [AllowAnonymous]
        public ActionResult uredi(int? id) {
            vlasnik v=null;
            if (id.HasValue)
            {
           v=  baza.vlasnikTab.FirstOrDefault(x => x.VlasnikID == id);
               ViewBag.Ovlasti=baza.ovlastiTab.ToList();
            }

            


        
        return View(v);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult uredi(vlasnik v)
        {

            ViewBag.Ovlasti = baza.ovlastiTab.ToList();

            if (ModelState.IsValid) {

                baza.Entry(v).State = System.Data.Entity.EntityState.Modified;
                baza.SaveChanges();

            }


          return  RedirectToAction("Korisnici");
           
        }
 //----------izbrisi korisnika---------------------------------
//potvrdi
       public ActionResult izbrisi(int? id)
        {
            if (id.HasValue) { 
            vlasnik v =baza.vlasnikTab.FirstOrDefault(x=>x.VlasnikID==id);
                if (v == null)
                {

                    return HttpNotFound();

                }
                else {

                    ViewBag.Title = "Jeste li sigurni da želite izbrisat korisnika " + v.PrezimeIme;
                    return View(v);
                }
            }


            return RedirectToAction("Korisnici");
        }
 //izbrisi
        [HttpPost]
        public ActionResult izbrisi(int id) {

            vlasnik v = baza.vlasnikTab.FirstOrDefault(x=>x.VlasnikID==id);
            if(v == null) { return HttpNotFound(); }
            baza.vlasnikTab.Remove(v);
            baza.SaveChanges();
            return RedirectToAction("Korisnici");
        
        }

//--------------------------promjeni lozinku-------------------------
        public ActionResult promjenaLozinke(int? id) { 
        if(id.HasValue)
            {
               var korisnik= baza.vlasnikTab.FirstOrDefault(x=>x.VlasnikID==id);

                if (korisnik != null) {


                    return View(korisnik);


                }

            }

            return RedirectToAction("Korisnici","Admin");
       
        
        }
       
    //spremi promjene
        [HttpPost]
        public ActionResult promjenaLozinke(vlasnik v)
        {
            if (!String.IsNullOrWhiteSpace(v.LozinkaA))
            {

                v.Lozinka=Misc.PasswordHelper.IzracunajHash(v.LozinkaA);




                if (ModelState.IsValid)
                {

                    baza.Entry(v).State = System.Data.Entity.EntityState.Modified;
                    baza.SaveChanges();
                    return RedirectToAction("Korisnici");

                }

            }

            return View(v);

        }




    }
}