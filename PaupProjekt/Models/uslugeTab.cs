using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{
    [Table("usluge")]
    public class uslugeTab
    {
        [Key]
        public int UslugaID { get; set; }
        [Display(Name = "naziv Usluga")]
        [Required(ErrorMessage = "{0} je obavezno")]
      
        public string nazivUsluga { get; set; }
        [Display(Name = "sifra")]
        [Required(ErrorMessage = "{0} je obavezno")]
      
        public decimal cijenaUsluga { get; set; }



    }
}