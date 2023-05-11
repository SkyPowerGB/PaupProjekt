using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{
    [Table("korisnici")]
    public class korisnici
    {
        [Key]
        public int idKorisnik { set; get; }

        public string korIme{ set; get; }

        public string imeKorisnik { set; get; }

        public string prezimeKorisnik { set; get; }

        public string sifraKorisnik { set; get; }

        public string emailKorisnik { set; get; }

    }
}