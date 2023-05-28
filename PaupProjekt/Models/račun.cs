using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace PaupProjekt.Models
{
    [Table("Račun")]
    public class račun
    {
        [Key]
        public int RačunID { get; set; }
        public int ServisID { get; set; }

        public DateTime DatumIzdavanja { get; set; }
        public decimal UkupanIznos { get; set; }
         
        public virtual servis Narudzba { get; set; }
    }
}