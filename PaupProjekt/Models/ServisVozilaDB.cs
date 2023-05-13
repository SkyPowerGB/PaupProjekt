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


       public DbSet<vlasnik> vlasnikTab { get; set; }

        public DbSet<vozilo> voziloTab { get; set; }

        public DbSet<servis> servisTab { get; set; }

        public DbSet<račun> racunTab { get; set; }
        public DbSet<ovlasti>  ovlastiTab { get; set; }
    }
}