using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{

    [Table("servis")]
    public class servis
    {
        [Key]

        [Display(Name ="ID")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public int ServisID { get; set; }

        [Display(Name = "VoziloID")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public int voziloID { get; set; }


        [Display(Name = "VlasnikID")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public int VlasnikID { get; set; }

        [Display(Name = "Datum")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public DateTime Datum { get; set; }

        [Display(Name = "OpisProblema")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string OpisProblema { get; set; }


        [Display(Name = "StatusServisa")]
        [Required(ErrorMessage = "{0} je obavezan")]
        
        public string StatusServisa { get; set; }

       public virtual vozilo VoziloVlasnika {  get; set; }

       public virtual vlasnik VlasnikVozila { get; set; }

    }
}