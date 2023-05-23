using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaupProjekt.Misc
{
    public class LogiraniKorisnikSerializeModel
    {
        public string Email { get; set; }
        public string PrezimeIme { get; set; }


        public string Ovlast { get; set; }
        internal void CopyFromUser(LogiraniKorisnik user) { 
       this.Email= user.Email;
            this.PrezimeIme= user.PrezimeIme;
            this.Ovlast= user.Ovlast;
        
        }

    }
}