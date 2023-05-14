using Microsoft.Ajax.Utilities;
using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class RadnikController : Controller
    {
        ServisVozilaDB db= new ServisVozilaDB();
        
        // GET: Radnik
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult klijenti() {

         var   baza = db.servisTab.ToList();

            return View(baza);
        }


    }
}