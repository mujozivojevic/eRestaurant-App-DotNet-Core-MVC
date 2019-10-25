using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class NarucilacController : Controller
    {
        private MyContext db;
        public NarucilacController(MyContext baza)
        {
            db = baza;
        }

        private NaruciociDodajVM GenerisiCmbStavke()
        {
            return new NaruciociDodajVM
            {
                gradLista = db.Grad.Select(x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.GradId.ToString()
                }).ToList()
            };
        }
        public IActionResult Prikazi()
        {
            List<NaruciociPrikaziViewModel> narucioci = db.Narucilac.Select(n => new NaruciociPrikaziViewModel
            {
                NarucilacId = n.NarucilacId.ToString(),
                ImePrezime = n.ImePrezime,
                KorisnickoIme = n.KorisnickiNalog.KorisnickoIme,
                Email = n.Email,
                Telefon = n.Telefon,
                Adresa = n.Adresa,
                grad = n.Grad
            }).ToList();
            NaruciociPrikaziVM model = new NaruciociPrikaziVM()
            {
                podaci = narucioci
            };
            return View("Prikazi", model);
        }
        public IActionResult Obrisi(int id)
        {
            Narucilac obrisati = db.Narucilac.SingleOrDefault(n => n.NarucilacId == id);

            if (obrisati == null)
                return View("Error");

            db.Remove(obrisati);
            db.SaveChanges();
            TempData["porukaDelete"] = "Uspjesno obrisan korisnik";

            return Redirect("/Narucilac/Prikazi");
        }

        public IActionResult DodajForm()
        {

            NaruciociDodajVM model = this.GenerisiCmbStavke();

            return View("DodajForm",model);
        }

        public IActionResult DodajSave(NaruciociDodajVM model)
        {
            Narucilac n = new Narucilac();

            n.ImePrezime = model.narucilac.ImePrezime;
            n.KorisnickiNalog.KorisnickoIme = model.narucilac.KorisnickiNalog.KorisnickoIme;
            n.Email = model.narucilac.Email;
            n.Telefon = model.narucilac.Telefon;
            n.Adresa = model.narucilac.Adresa;
            n.GradId = model.narucilac.GradId;
            
            db.Add(n);
            db.SaveChanges();

            TempData["porukaSuccess"] = "Uspjesno dodan zapis";
            return Redirect("/Narucilac/Prikazi");
        }
    }
}