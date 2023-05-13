using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{
    [Table("vozilo")]
    public class vozilo
    {

        [Key]
        public int VoziloId { get; set; }

        public string Marka { get; set; }

        public string Model { get; set; }

        public int GodinaProizvodnje { get; set; }

        public string Registracija { get; set; }


    }
}