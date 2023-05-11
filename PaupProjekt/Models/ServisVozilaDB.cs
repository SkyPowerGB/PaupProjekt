using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PaupProjekt.Models
{


    [DbConfigurationType(typeof(MySqlEFConfiguration))]

    public class ServisVozilaDB:DbContext 
    {

        public DbSet<korisnici> KorisniciB { get; set; }



    }
}