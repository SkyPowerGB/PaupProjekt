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

        [Display(Name = "Datum servisa")]
        public DateTime? DatumServisa { get; set; }

        [Display(Name = "Opis servisa")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string OpisProblema { get; set; }


        [Display(Name = "Status Servisa")]
        [Required(ErrorMessage = "{0} je obavezan")]
        
        public string StatusServisa { get; set; }


        [Display(Name ="Slika kvara vozila")]    
        public string slikaVozila { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }



        public virtual vozilo VoziloVlasnika {  get; set; }

       public virtual vlasnik VlasnikVozila { get; set; }


    }
}

