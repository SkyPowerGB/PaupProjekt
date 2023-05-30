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
        // GET: Racun
        public ActionResult IzavanjeRacuna(int? idNarudzbe,int? idUsluge)
        {
            bool novi = false;

            var racunNarudzbe = (baza.racunTab.FirstOrDefault(x => x.ServisID == idNarudzbe));
            if (!idNarudzbe.HasValue) { return HttpNotFound("Usli u nema vrijednost"+idNarudzbe); }
            if (racunNarudzbe==null) { racunNarudzbe = new račun(); racunNarudzbe.ServisID = (int)idNarudzbe;
                novi= true;
                ViewBag.Naslov = "Izrada novog računa";
                racunNarudzbe.UkupanIznos = 0;
                baza.racunTab.Add(racunNarudzbe);
                baza.SaveChanges();
            }

            if(idUsluge.HasValue)
            {
                ListaUslugaTab novaUslugaL = new ListaUslugaTab();
                novaUslugaL.UslugaID = (int)idUsluge;
                novaUslugaL.koef = 1;
                novaUslugaL.RačunID =racunNarudzbe.RačunID;

                baza.ListaUslugaTab.Add(novaUslugaL);
                baza.SaveChanges();
                baza=new ServisVozilaDB();

            }
            if (!novi) { ViewBag.Naslov = "Uredivanje računa"; }
         
            ViewBag.idNarudzbe= idNarudzbe;
            var listaUsluga = baza.ListaUslugaTab.Where(x=>x.RačunID==racunNarudzbe.RačunID).ToList();
           
            var uk = listaUsluga.Sum(x => x.Usluge.cijenaUsluga);


         
            racunNarudzbe.UkupanIznos = 0+uk;
            ViewBag.racun=racunNarudzbe;
         

            return View(listaUsluga);
        }



       

        public ActionResult DodajUslugu(int id) {

            
               
            



            ViewBag.id = id;
            return View(baza.uslugeTab.ToList());
        }

        public ActionResult MakniUslugu(int idListe)
        {
            var listaUsluga = baza.ListaUslugaTab.FirstOrDefault(x=>x.idListe==idListe);
            var idNarudzbe = listaUsluga.Račun.Narudzba.ServisID;
            baza.ListaUslugaTab.Remove(listaUsluga);
            baza.SaveChanges();
            return RedirectToAction("IzavanjeRacuna", new { idNarudzbe=idNarudzbe });

         
        }



       


    }
}