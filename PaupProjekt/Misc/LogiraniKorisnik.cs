using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace PaupProjekt.Misc
{
    public class LogiraniKorisnik : IPrincipal  
    {

        public string Email { get; set; }
        public string PrezimeIme { get; set; }
        public string Ovlast { get; set; }

        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (Ovlast == role) return true;
            return false;
        }
        public LogiraniKorisnik(vlasnik v) {
            Identity = new GenericIdentity(v.Email);
            Email=v.Email;
            PrezimeIme = v.PrezimeIme;
            Ovlast = v.sifraOvlast;
        
        }
        public LogiraniKorisnik(string email) { 
        Identity =new GenericIdentity(email);
           Email=email;
        }


    }
}