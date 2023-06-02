using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace PaupProjekt.Models
{
    [Table("Račun")]
    public class račun
    {
        [Key]
        [Display(Name = "idRačuna")]
        public int RačunID { get; set; }

        [Display(Name = "idNarudžbe")]
        public int ServisID { get; set; }
        [Display(Name = "Datum izdavanja")]
        public DateTime DatumIzdavanja { get; set; }
        [Display(Name = "Ukupan Iznos")]
        public decimal UkupanIznos { get; set; }
      
        public virtual servis Narudzba { get; set; }

        public bool Izdan { get; set; }
    }
}