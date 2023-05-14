using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PaupProjekt.Models
{
    public class Provjere
    {

        public static bool checkEMail( string email) {

            bool hasM = false;
            bool hasD= false;
            

            foreach (char c in email)
            {



                if (c == '@')
                {
                    hasM = true;
                }
                if(hasM)
                {
                    if (c == '.') { 
                    hasD = true;
                    }

                }

            }
            if( hasD && hasM) { return true; }

            return false;
        }
    }








}