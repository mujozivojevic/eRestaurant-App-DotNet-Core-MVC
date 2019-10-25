using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class ZaposlenikController : Controller
    {

        private MyContext db;
        public ZaposlenikController(MyContext baza)
        {
            db = baza;
        }


        [HttpGet]
        public IActionResult Index()
        {

            ViewData["grad"] = db.Grad.ToList();

            ViewData["radnoMjesto"] = db.RadnoMjesto.ToList();

            ViewData["zaposlenik"] = db.Zaposlenik.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Index(int GradId, int RadnoMjestoId, string Ime,
            string Prezime, string JMBG, string telefon, string Email, string KorisnickoIme, string Lozinka, DateTime datumRodjenja)
        {

            Zaposlenik j = new Zaposlenik();
            j.Ime = Ime;
            j.Prezime = Prezime;
            j.KorisnickiNalog.KorisnickoIme = KorisnickoIme;
            j.KorisnickiNalog.Lozinka = Lozinka;
            j.Email = Email;
            j.Telefon = telefon;
            j.JMBG = JMBG;
            j.DatumRodjenja = datumRodjenja;

            j.GradId = GradId;
            j.RadnoMjestoId = RadnoMjestoId;

            db.Zaposlenik.Add(j);
            db.SaveChanges();


            ViewData["zaposlenik"] = db.Zaposlenik.ToList();
            ViewData["grad"] = db.Grad.ToList();
            ViewData["radnoMjesto"] = db.RadnoMjesto.ToList();

            return View();
        }
        public ActionResult Uredi(int Id)
        {

            Zaposlenik j = db.Zaposlenik.Where(x => x.Id == Id).SingleOrDefault();

            if (j != null)
            {
                ViewData["grad"] = db.Grad.ToList();
                ViewData["radnoMjesto"] = db.RadnoMjesto.ToList();
                ViewData["zaposlenik"] = db.Zaposlenik.ToList();
                return View(j);
            }

            return RedirectToAction("Index", "Index");
        }
        [HttpPost]
        public ActionResult Uredi(int Id, int GradId, int RadnoMjestoId, string Ime,
            string Prezime, string JMBG, string telefon, string Email, string KorisnickoIme, string Lozinka, DateTime datumRodjenja)
        {

            Zaposlenik j = db.Zaposlenik.Where(x => x.Id == Id).SingleOrDefault();

            if (j != null)
            {
                j.Ime = Ime;
                j.Prezime = Prezime;
                j.JMBG = JMBG;
                j.Telefon = telefon;
                j.Email = Email;
                j.KorisnickiNalog.KorisnickoIme = KorisnickoIme;
                j.KorisnickiNalog.Lozinka = Lozinka;
                j.DatumRodjenja = datumRodjenja;

                j.GradId = GradId;
                j.RadnoMjestoId = RadnoMjestoId;

                db.SaveChanges();

                ViewData["grad"] = db.Grad.ToList();
                ViewData["radnoMjesto"] = db.RadnoMjesto.ToList();
                ViewData["zaposlenik"] = db.Zaposlenik.ToList();

                return RedirectToAction("Uredi", "Zaposlenik", new { Id = Id });
                /// 
              //  return RedirectToAction("Index", "test");
            }

            return RedirectToAction("Index", "Index");
        }
        [HttpGet]
        public IActionResult Prikazi()
        {

            ViewData["grad"] = db.Grad.ToList();

            ViewData["radnoMjesto"] = db.RadnoMjesto.ToList();

            ViewData["zaposlenik"] = db.Zaposlenik.ToList();

            return View();
        }

    }
}