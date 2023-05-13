using System;
using System.Collections.Generic;
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
        public int ServisID { get; set; }

        public int voziloID { get; set; }

        public int VlasnikID { get; set; }

        public DateTime Datum { get; set; }

        public string OpisProblema { get; set; }

        public string StatusServisa { get; set; }

    }
}