using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EF;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RadnoMjestoController : Controller
    {

        private MyContext db;
        public RadnoMjestoController(MyContext baza)
        {
            db = baza;
        }
        public IActionResult Prikazi()
        {

            List<RadnoMjesto> radnaMjesta = db.RadnoMjesto.ToList();

            ViewData["radnaMjesta-kljuc"] = radnaMjesta;
            return View();
        }

        public IActionResult DodajRadnoMjesto(string radnoMjestoNaziv, string radnoMjestoOpis)
        {

            RadnoMjesto x = new RadnoMjesto();
            // ViewData["grad-kljuc"] = x;
            x.Naziv = radnoMjestoNaziv;
            x.Opis = radnoMjestoOpis;

            db.Add(x);
            db.SaveChanges();
            return View();
        }

        public IActionResult Obrisi(int Id)
        {

            RadnoMjesto g  = db.RadnoMjesto.Find(Id);
            if (g == null)
            {
                return Content("Radno mjesto ne postoji.");
            }
            db.Remove(g);

            db.SaveChanges();
            return Redirect("/RadnoMjesto/Prikazi");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}