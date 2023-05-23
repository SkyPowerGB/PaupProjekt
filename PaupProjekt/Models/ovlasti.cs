using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{

    [Table("ovlasti")]
    public class ovlasti
    {

        [Key]
        [Display(Name ="sifra")]
        [Required(ErrorMessage ="{0} JE OBAVEZNA")]
        [Column("sifra")]
        public string sifra { get; set; }

        [Display(Name = "naziv")]
        [Required(ErrorMessage = "{0} JE OBAVEZNA")]
        [Column("naziv")]
        public string naziv { get; set; }


    }
}