using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{
    public class Liste
    {
        public static List<String> ListaOvlasti { get; set; }

        public static List<String> ListaFiltera { get; set; }
        public static List<String> ListaFilteraCijene { get; set; }

        public static bool vrijednostiPost = false;
        static Liste()
        {

            if (!vrijednostiPost)
            {
                ListaOvlasti = new List<string>
                {
                    "AD",
                    "MO",
                    "KO"
                };

                ListaFiltera= new List<string>
                {
                    "NULL",
                    "Uzlazno",
                    "Silazno"
                };

                ListaFilteraCijene = new List<string>
                {
                    "NULL",
                    "Cijena od manje prema većoj",
                    "Cijena od veće prema manjoj"
                };

                vrijednostiPost = true;



            }




        }



    }
}