using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PaupProjekt.Models
{
    public class KorisnickaKartica
    {
        //staro ostaci pokusaja izrade prijave

        [Required]
        [Display(Name = "Email ")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Lozinka { get; set; }
    }
}