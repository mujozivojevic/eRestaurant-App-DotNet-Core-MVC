using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class SastojciController : Controller
    {
        private MyContext db;

        public SastojciController(MyContext baza)
        {
            db = baza;
        }
        public IActionResult Prikazi()
        {
            SastojciPrikaziVM model = new SastojciPrikaziVM
            {
                htmlRows = db.Sastojci.Select(x => new SastojciPrikaziVM.Row
                {
                   Id = x.Id,
                   Naziv = x.Naziv,
                   Opis = x.Opis
                }).ToList()
            };

            return View("Prikazi", model);
        }

        public IActionResult DodajForm()
        {
            SastojciDodajVM model = new SastojciDodajVM();

            return View("Dodaj", model);
        }

        public IActionResult DodajSave(SastojciDodajVM model)
        {
            Sastojci s = new Sastojci();
            s.Naziv = model.Sastojci.Naziv;
            s.Opis = model.Sastojci.Opis;

            db.Add(s);
            db.SaveChanges();

            TempData["porukaAdd"] = "Uspjesno ste dodali sastojak";
            return Redirect("/Sastojci/Prikazi");
        }

        public IActionResult Obrisi(int id)
        {
            Sastojci s = db.Sastojci.Where(x => x.Id == id).FirstOrDefault();
            db.Remove(s);
            db.SaveChanges();


            TempData["porukaDelete"] = "Uspjesno ste obrisali sastojak";
            return Redirect("/Sastojci/Prikazi");
        }
    }
}