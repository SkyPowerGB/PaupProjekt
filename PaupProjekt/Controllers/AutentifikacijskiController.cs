using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Asn1.Misc;
using PaupProjekt.Misc;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace PaupProjekt.Controllers
{
    public class AutentifikacijskiController : Controller
    {
        // GET: Autentifikacijski
        ServisVozilaDB baza = new ServisVozilaDB();

 //----------------Registracija------------------------------
        [HttpGet]
        [AllowAnonymous]
  
        public ActionResult Registracija()
        {

            ViewBag.novo = false;


            return View( new vlasnik() ) ;
        }
//spremi podatke
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registracija( vlasnik v)
        {

            ViewBag.novo = true;



            v.sifraOvlast = "KO";
            if (String.IsNullOrEmpty(v.LozinkaA))
            {
                ModelState.AddModelError("LozinkaA", "Lozinka ne moze biti prazno");
            }
            else
            {
                v.Lozinka = Misc.PasswordHelper.IzracunajHash(v.LozinkaA);

                var emailZauzet = baza.vlasnikTab.Any(x => x.Email == v.Email);
                if (emailZauzet)
                {
                    ModelState.AddModelError("Email", "Email je već zauzet");
                }
                if (ModelState.IsValid)
                {

                    baza.vlasnikTab.Add(v);
                    baza.SaveChanges();
                 return  RedirectToAction("RegistracijaUspjesna");
                   
                }
            }


            
              
          var ovlasti = baza.ovlastiTab.OrderBy(x=>x.naziv).ToList();
            ViewBag.Ovlast = ovlasti;
           
            return View(v);
            
        }
        
  //----------Registracija Uspješna-------------------------      
        [AllowAnonymous]
        public ActionResult RegistracijaUspjesna() {

            return View();
        }


 //-------Prijava-----------------------------------
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            KorisnickaKartica model = new KorisnickaKartica();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);

            
        }

//uzmi provjeri  i spremiKorPodatke /javi netocna lozinka ili email

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login( KorisnickaKartica kartica,string returnUrl)
        {
          
            

            if (ModelState.IsValid) {
                var korisnici = baza.vlasnikTab.FirstOrDefault(x => x.Email == kartica.Email);
                if(korisnici != null) {
                    var lozinkaTocna = korisnici.Lozinka == PasswordHelper.IzracunajHash(kartica.Lozinka);
                    if (lozinkaTocna) {

                        LogiraniKorisnik prijavljeniKorisnik = new LogiraniKorisnik(korisnici);

                        LogiraniKorisnikSerializeModel serializeModel = new LogiraniKorisnikSerializeModel();
                        serializeModel.CopyFromUser(prijavljeniKorisnik);
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string korisnickiPodaci = serializer.Serialize(serializeModel);

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                            1,
                              prijavljeniKorisnik.Identity.Name,
                               DateTime.Now,
                            DateTime.Now.AddDays(1),
                            false,
                               korisnickiPodaci
                            );
                        string ticketEncrypted = FormsAuthentication.Encrypt(authTicket);

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketEncrypted);
                        Response.Cookies.Add(cookie);
                        if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Korisnik");
                    }
                }


            }
            ModelState.AddModelError("", "Neispravno korisničko ime ili lozinka");
            return View(kartica);
        }


        //-----tu bi trebala biti promjena  Lozinke korisnika (NIJE NAPRAVLJENO)
        //bilo bi dobro izvest tak da i admin ima tu tj ka ne tre dva action resulta
        [Authorize]
       
        public ActionResult PromjenaLozinke()
        {

            var Email = User.Identity.Name;
            var Trazeni = baza.vlasnikTab.FirstOrDefault(x=>x.Email==Email);
            if(Trazeni == null ) { return HttpNotFound(); }


            return View(Trazeni);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult PromjenaLozinke(vlasnik v)
        {
            if (!string.IsNullOrWhiteSpace(v.LozinkaA) && v.LozinkaA != v.LozinkaPon)
            {
                ModelState.AddModelError("", "Lozinke se ne podudaraju.");
                return View(v);
            }
            v.Lozinka=PasswordHelper.IzracunajHash(v.LozinkaA);
            if(ModelState.IsValid)
            {
                baza.Entry(v).State = System.Data.Entity.EntityState.Modified;
                baza.SaveChanges();
                return RedirectToAction("Index","Korisnik");
            }


            return View(v);
        }

   //-----------------------------------odjava----------------  
        public ActionResult Odjava()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Gost");
        }

    }
}