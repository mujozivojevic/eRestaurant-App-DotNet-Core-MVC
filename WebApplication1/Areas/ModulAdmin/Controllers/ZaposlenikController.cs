using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Areas.ModulAdmin.ViewModels.Zaposlenik;
using WebApplication1.Helper;

namespace WebApplication1.Areas.ModulAdmin.Controllers
{
    [Autorizacija(zaposlenik: true, korisnik: false)]

    [Area("ModulAdmin")]
    [Route("/ModulAdmin/Zaposlenik")]
    public class ZaposlenikController : Controller
    {

        private MyContext db;
        public ZaposlenikController(MyContext baza) {
            db = baza;
        }

        [Route("/ModulAdmin/Zaposlenik/List")]
        public IActionResult List()
        {


            ZaposlenikListVM model = new ZaposlenikListVM
            {
                htmlRows = db.Zaposlenik.Select(s => new ZaposlenikListVM.Row
                {
                    ZaposlenikId = s.Id,
                    ImePrezime = s.Ime + " " + s.Prezime,
                    JMBG = s.JMBG,
                    Grad = s.Grad.Naziv,
                    Adresa = s.Adresa,
                    DatumRodjenja = s.DatumRodjenja.ToString("dd.MM.yyyy"),
                    Email = s.Email,
                    RadnoMjesto = s.RadnoMjesto.Naziv,
                    Telefon = s.Telefon,
                    KorisnickoIme = s.KorisnickiNalog.KorisnickoIme,
                    Lozinka = s.KorisnickiNalog.Lozinka
                }).ToList()
            };
            return View(model);
        }

        [Route("/ModulAdmin/Zaposlenik/Obrisi")]
        public IActionResult Obrisi(int id)
        {
            Zaposlenik obrisati = db.Zaposlenik.SingleOrDefault(z => z.Id == id);

            if (obrisati == null)
                return View("Error");

            db.Remove(obrisati);
            db.SaveChanges();
  
            return RedirectToAction("List");
        }

        [Route("/ModulAdmin/Zaposlenik/Dodaj")]
        public IActionResult Dodaj()
        {
            ZaposlenikDodajVM model = new ZaposlenikDodajVM();
            GenerisiCmbStavke(model);
           
            return View(model);
        }

        [Route("/ModulAdmin/Zaposlenik/Uredi")]
        [HttpGet]

        public IActionResult Uredi(int id)
        {
            Zaposlenik z = db.Zaposlenik.Find(id);
            KorisnickiNalog kn = db.KorisnickiNalog.Where(w => w.Id == z.KorisnickiNalogId).SingleOrDefault();


            ZaposlenikDodajVM model = new ZaposlenikDodajVM();
            GenerisiCmbStavke(model);

            model.Ime = z.Ime;
            model.Prezime = z.Prezime;
            model.Adresa = z.Adresa;
            model.Email = z.Email;
            model.KorisnickoIme = z.KorisnickiNalog.KorisnickoIme;
            model.Lozinka = z.KorisnickiNalog.Lozinka;
            model.Telefon = z.Telefon;
            model.JMBG = z.JMBG;
            model.DatumRodjenja = z.DatumRodjenja;
            model.GradID = z.GradId;
            model.RadnoMjestoId = z.RadnoMjestoId;

            return View(model);
        }

        [Route("/ModulAdmin/Zaposlenik/DodajSave")]
        [HttpPost]
        public IActionResult DodajSave(ZaposlenikDodajVM model)
        {

            if (ModelState.IsValid)
            {
                Zaposlenik z = db.Zaposlenik.Find(model.Id);
                KorisnickiNalog kn = new KorisnickiNalog();

                if (z == null)
                {
                    z = new Zaposlenik();
            
                    db.Add(kn);
                    db.Add(z);
                }

                kn.KorisnickoIme = model.KorisnickoIme;
                kn.Lozinka = model.Lozinka;

                z.Ime = model.Ime;
                z.Prezime = model.Prezime;
                z.Email = model.Email;
                z.Telefon = model.Telefon;
                z.KorisnickiNalogId = kn.Id;
                z.Adresa = model.Adresa;
                z.JMBG = model.JMBG;
                z.DatumRodjenja = model.DatumRodjenja;
                z.GradId = model.GradID;
                z.RadnoMjestoId = model.RadnoMjestoId;

                db.SaveChanges();

                return RedirectToAction("List");
            }
            else
            {
                GenerisiCmbStavke(model);
                return View("Dodaj", model);

            }
        }

        [Route("/ModulAdmin/Zaposlenik/UrediSave")]
        [HttpPost]
        public IActionResult UrediSave(ZaposlenikDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Zaposlenik z = db.Zaposlenik.Find(model.Id);
                KorisnickiNalog kn = db.KorisnickiNalog.Where(w=> w.Id == z.KorisnickiNalogId).FirstOrDefault();

                if (z == null)
                {
                    z = new Zaposlenik();
                    kn = new KorisnickiNalog();
                    db.Add(kn);
                    db.Add(z);
                }

                kn.KorisnickoIme = model.KorisnickoIme;
                kn.Lozinka = model.Lozinka;

                z.Ime = model.Ime;
                z.Prezime = model.Prezime;
                z.Adresa = model.Adresa;
                z.Email = model.Email;
                z.Telefon = model.Telefon;
                z.JMBG = model.JMBG;
                z.DatumRodjenja = model.DatumRodjenja;
                z.GradId = model.GradID;
                z.RadnoMjestoId = model.RadnoMjestoId;

                db.SaveChanges();

                return RedirectToAction("List");
            }
            else
            {
                GenerisiCmbStavke(model);
                return View("Uredi", model);

            }
        }

        private void GenerisiCmbStavke(ZaposlenikDodajVM model)
        {
            model.GradLista = db.Grad.Select(s => new SelectListItem
            {
                Text = s.Naziv,
                Value = s.GradId.ToString()
            }).ToList();
            model.RadnoMjestoList = db.RadnoMjesto.Select(s => new SelectListItem
            {
                Text = s.Naziv,
                Value = s.Id.ToString()
            }).ToList();
        }
    }
}