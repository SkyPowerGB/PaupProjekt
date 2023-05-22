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
        [Display(Name = "Vlasnik ID")]

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
        [Required(ErrorMessage = "{0} je obavezno")]


        public string Lozinka { get; set; }





        [Display(Name = "Lozinka")]

        [Required]
        [NotMapped]
        public string LozinkaA { get; set; }




        [Display(Name = "Ponovite lozinku")]

        [Required]
        [NotMapped]
        [Compare("LozinkaA", ErrorMessage = "Lozinke se ne podudaraju")]
        public string LozinkaPon { get; set; }



        public string PrezimeIme {
            get {
                return Prezime + " " + Ime;
            }

        }
        [Required]
        [Column("ovlast")]
        [ForeignKey("Ovlast")]
        public string sifraOvlast { get; set; }

        public virtual ovlasti Ovlast{get; set;}






    }
}