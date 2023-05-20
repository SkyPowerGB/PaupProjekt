using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{
    public class KorisnickaKartica
    {
        public static int idKorisnika { get; set; }
        public static byte razinaPristupa { get; set; }

        KorisnickaKartica(int idKorisnika ,string ovlast) {
            if (!string.IsNullOrWhiteSpace(ovlast))
            {

                switch(ovlast)
                {

                    case "Admin":

                        razinaPristupa = 3;
                        break;

                    case "Radnik":
                        razinaPristupa = 2;
                        break;

                    case "Korisnik":
                        razinaPristupa = 1;
                        break;

                    default:
                        razinaPristupa = 0;

                        break;


                }
                


                KorisnickaKartica.idKorisnika = idKorisnika;

            }


        
        }

    }
}