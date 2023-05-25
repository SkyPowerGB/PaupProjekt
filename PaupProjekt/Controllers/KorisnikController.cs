using PaupProjekt.Misc;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class KorisnikController : Controller
    {
        // GET: Korisnik
        ServisVozilaDB baza = new ServisVozilaDB();
        [Authorize]
        public ActionResult Index(int? id)
        {
           var Email=HttpContext.User.Identity.Name;
           

           vlasnik Trazeni= baza.vlasnikTab.FirstOrDefault(x=>x.Email==Email);


            var servisTrazenog = baza.servisTab.FirstOrDefault(x => x.VlasnikID == Trazeni.VlasnikID);



           // dsfsd
            return View(servisTrazenog);
        }

        public ActionResult Usluge() { 
        
        return View();  
        }
        public ActionResult Profil() {
        




        return View();
        }



        public ActionResult Vozila() {

            var Email = HttpContext.User.Identity.Name;
            vlasnik Trazeni = baza.vlasnikTab.FirstOrDefault(x => x.Email == Email);
            var Vozila = baza.voziloTab.Where(x=>x.voziloVlasnika.VlasnikID==Trazeni.VlasnikID).ToList();
            
            return View(Vozila);
        }

        public ActionResult izbrisiVozilo(int? id)
        {
            if(id.HasValue)
            {

                ViewBag.Title = "Jeste li sigurni da želite izbrisat Vozilo ";

                vozilo v=  baza.voziloTab.FirstOrDefault(x=>x.VoziloId==id);
                if (v != null)
                {
                    return View(v);
                }
            }


            return View();
        }

        [HttpPost]
        public ActionResult izbrisiVozilo(vozilo voz)
        {
            var servisi = baza.servisTab.FirstOrDefault(x => x.voziloID == voz.VoziloId);
            if (servisi == null) {
               
                baza.voziloTab.Remove(voz);
                baza.SaveChanges();

            }



            return View("Greška");
        }

    }
}