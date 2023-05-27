using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{
    [Table("listausluga")]
    public class ListaUslugaTab
    {
        [Key]
        public int idListe { get; set; }
        public int kol { get; set; }

        public decimal koef { get; set; }

      
        public int RačunID { get; set; }



        public int UslugaID { get; set; }


        public virtual uslugeTab Usluge{ get;set;}

        public virtual račun Račun { get; set; }


    }
}