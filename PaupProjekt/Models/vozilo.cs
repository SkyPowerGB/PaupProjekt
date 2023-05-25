using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PaupProjekt.Models
{
    [Table("vozilo")]
    public class vozilo
    {

        [Key]
        [Display(Name = "ID Vozila")]
        public int VoziloId { get; set; }

        [Display(Name = "Marka vozila")]
        public string Marka { get; set; }

        [Display(Name = "Model vozila")]
        public string Model { get; set; }
        [Display(Name = "Godina proizvodnje vozila")]
        public int GodinaProizvodnje { get; set; }

        [Display(Name = "Registracija vozila")]
        public string Registracija { get; set; }

        [Column("VlasnikID")]
        [Display(Name = "ID vlasnika")]
        public int VlasnikID { get; set; }

        [ForeignKey("VlasnikID")]
        public virtual vlasnik voziloVlasnika { get; set; }

        public string MarkaRegitracija { get { return Marka + " " + Registracija; } }

    }
}