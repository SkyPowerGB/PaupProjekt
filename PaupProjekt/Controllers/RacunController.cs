using Microsoft.Ajax.Utilities;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class RacunController : Controller
    {
        ServisVozilaDB baza = new ServisVozilaDB();
        
        //-------------------Kreiranje / uređivanje (ne izdanog) Računa-----

        public ActionResult IzavanjeRacuna(int? idNarudzbe,int? idUsluge)
        {
            bool novi = false;

            var racunNarudzbe = (baza.racunTab.FirstOrDefault(x => x.ServisID == idNarudzbe));
          
            var uslugeNarudzbe = baza.ListaUslugaTab.Where(x=>x.RačunID==racunNarudzbe.RačunID);
            //narudzba ne postoji
            if (!idNarudzbe.HasValue) { return HttpNotFound("Usli u nema vrijednost"+idNarudzbe); }

         
            //izrada novoga racuna ako je prvi put
            if (racunNarudzbe==null) {
                racunNarudzbe = new račun(); racunNarudzbe.ServisID = (int)idNarudzbe;
                novi= true;
                ViewBag.Naslov = "Izrada novog računa";
                racunNarudzbe.UkupanIznos = 0;
                baza.racunTab.Add(racunNarudzbe);
                baza.SaveChanges();
            }
         //ako je racun izdan nemoguce je mjenjat
            if (racunNarudzbe.Izdan)
            {

                return RedirectToAction("klijenti","Radnik");
            }

            //dodavanje nove usluge
            if (idUsluge.HasValue)
            {
                if (uslugeNarudzbe.FirstOrDefault(x => x.UslugaID == idUsluge) != null)
                {
                    var usluga = uslugeNarudzbe.FirstOrDefault(x => x.UslugaID == idUsluge);
                    usluga.kol++;

                    baza.Entry(usluga).State = System.Data.Entity.EntityState.Modified;
                    baza.SaveChanges();
                }
                else
                {

                    ListaUslugaTab novaUslugaL = new ListaUslugaTab();
                    novaUslugaL.UslugaID = (int)idUsluge;
                    novaUslugaL.koef = 1;
                    novaUslugaL.kol = 1;
                    novaUslugaL.RačunID = racunNarudzbe.RačunID;

                    baza.ListaUslugaTab.Add(novaUslugaL);
                    baza.SaveChanges();
                }
                 
                baza=new ServisVozilaDB();

            }
            if (!novi) { ViewBag.Naslov = "Uredivanje računa"; }
            ViewBag.idRacuna = racunNarudzbe.RačunID;
            ViewBag.idNarudzbe= idNarudzbe;

            var listaUsluga = baza.ListaUslugaTab.Where(x=>x.RačunID==racunNarudzbe.RačunID).ToList();
           

            //uk - ukupni iznos iz listeUsluga tog racuna
            var uk = listaUsluga.Sum(x => x.Usluge.cijenaUsluga*x.kol);
           

     //postavljanje ukupnoga iznosa
            racunNarudzbe.UkupanIznos = 0+uk;
            ViewBag.racun=racunNarudzbe;


            return View(listaUsluga);
        }

     //------------------Dodaj uslugu na listu usluga Računa-------------------------

        public ActionResult DodajUslugu(int id) {

            
               
            



            ViewBag.id = id;
            return View(baza.uslugeTab.ToList());
        }


        //------------------Makni uslugu sa liste usluga Računa-------------------------
        public ActionResult MakniUslugu(int idListe)
        {
            var listaUsluga = baza.ListaUslugaTab.FirstOrDefault(x=>x.idListe==idListe);
            var idNarudzbe = listaUsluga.Račun.Narudzba.ServisID;
           
            baza.ListaUslugaTab.Remove(listaUsluga);
            baza.SaveChanges();
            return RedirectToAction("IzavanjeRacuna", new { idNarudzbe=idNarudzbe });

         
        }

        //------------------Potvrda (izdaj) Račun-------------------------

        //nakon što je izdan više ga nije moguće uređivat 

        [HttpGet]
        public ActionResult IzdajRacun(int idRacuna) {

        ViewBag.uslugeNarudzbe =  baza.ListaUslugaTab.Where(x=>x.RačunID==idRacuna).ToList();
          var racun = baza.racunTab.FirstOrDefault(x => x.RačunID == idRacuna);


        return View(racun);
        }

     
        public ActionResult IzdajRacun(račun r)
        {
            r.Izdan = true;
            r.DatumIzdavanja=DateTime.Now;
            baza.Entry(r).State= System.Data.Entity.EntityState.Modified;
            baza.SaveChanges();
            return RedirectToAction("klijenti","Radnik");
        }


    }
}