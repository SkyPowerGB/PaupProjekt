using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{
    public class Liste
    {
        public static List<String> ListaOvlasti { get; set; }

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
                vrijednostiPost = true;
            }




        }



    }
}