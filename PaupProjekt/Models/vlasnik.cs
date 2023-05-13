using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{
    [Table("vlasnik")]
    public class vlasnik
    {
        [Key]
        public int VlasnikID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }

        public string Lozinka{ get; set; }
        public string ovlast { get; set; }

    }
}