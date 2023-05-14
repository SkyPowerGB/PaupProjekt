using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PaupProjekt.Models
{
    [Table("vlasnik")]
    public class vlasnik
    {
        [Key]
       [ Display(Name = "Vlasnik ID")]
      
        public int VlasnikID { get; set; }

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Ime { get; set; }

        
        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Prezime { get; set; }


        [Display(Name = "email")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Email { get; set; }



        [Display(Name = "lozinka")]
        [Required(ErrorMessage ="{0} je obavezno")]

        [StringLength(255,MinimumLength =8 ,ErrorMessage ="{0} mora biti duljine minimalno {2} znakova")]
        public string Lozinka{ get; set; }


       
        public string ovlast { get; set; }

    }
}