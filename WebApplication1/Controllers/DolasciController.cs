using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EF;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DolasciController : Controller
    {
        private MyContext db;
        public DolasciController(MyContext baza)
        {
            db = baza;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["zaposlenik"] = db.Zaposlenik.ToList();

            ViewData["dolasci"] = db.Dolasci.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Index(int ZaposlenikID, DateTime Dolazak, DateTime Odlazak)
        {

            Dolasci j = new Dolasci();
            j.Dolazak = Dolazak;
            j.Odlazak = Odlazak;

            j.ZaposlenikId = ZaposlenikID;

            db.Dolasci.Add(j);
            db.SaveChanges();


            ViewData["zaposlenik"] = db.Zaposlenik.ToList();
            ViewData["dolasci"] = db.Dolasci.ToList();
            

            return View();
        }

        [HttpGet]
        public IActionResult Prikazi()
        {

            ViewData["zaposlenik"] = db.Zaposlenik.ToList();

            ViewData["dolasci"] = db.Dolasci.ToList();

            return View();
        }
    }
}