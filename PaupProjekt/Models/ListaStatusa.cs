using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{


    public class ListaStatusaE
    {

        public static List<String> DopusteneVrijednosi { get; set; }

        public static bool vrijednostiPost = false;
        static ListaStatusaE()
        {

            if (!vrijednostiPost)
            {
                DopusteneVrijednosi = new List<string>
                {
                    
                    "Na Čekanju",
                    "Zaprimljen",
                    "U tijeku",
                    "Završen",
                    "Otkazan"
                };
                vrijednostiPost = true;
            }




        }

    }
}