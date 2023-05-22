using PaupProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaupProjekt.Controllers
{
    public class AdminController : Controller
    {
        ServisVozilaDB baza = new ServisVozilaDB();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Korisnici() {
            var kor = baza.vlasnikTab.ToList();
        
        return View(kor);
        
        }
    }
}