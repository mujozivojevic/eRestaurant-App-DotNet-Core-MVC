using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Areas.ModulAdmin.ViewModels.Narucilac;
using WebApplication1.Helper;

namespace WebApplication1.Areas.ModulAdmin.Controllers
{
    [Autorizacija(zaposlenik: true, korisnik: false)]

    [Area("ModulAdmin")]
    [Route("/ModulAdmin/Narucilac")]
    public class NarucilacController : Controller
    {
        private MyContext db;
        public NarucilacController(MyContext baza)
        {
            db = baza;
        }

   
        [Route("/ModulAdmin/Narucilac/List")]
        public IActionResult List()
        {

            NarucilacListVM model = new NarucilacListVM
            {
                htmlRows = db.Narucilac.Select(n => new NarucilacListVM.Row
                {
                    NarucilacId = n.NarucilacId.ToString(),
                    ImePrezime = n.ImePrezime,
                    KorisnickoIme = n.KorisnickiNalog.KorisnickoIme,
                    Email = n.Email,
                    Telefon = n.Telefon,
                    Adresa = n.Adresa,
                    GradNaziv = n.Grad.Naziv
                }).ToList()
            };

            return View(model);
        }
        [Route("/ModulAdmin/Narucilac/Obrisi")]
        [HttpGet]
        public IActionResult Obrisi(int id)
        {
            Narucilac obrisati = db.Narucilac.SingleOrDefault(n => n.NarucilacId == id);

            if (obrisati == null)
                return View("Error");

            db.Remove(obrisati);
            db.SaveChanges();
            TempData["porukaDelete"] = "Uspjesno obrisan korisnik";

            return RedirectToAction("List");
        }


        [Route("/ModulAdmin/Narucilac/Dodaj")]
        public IActionResult Dodaj()
        {

            var model = new NarucilacDodajVM();
            GenerisiCmbStavke(model);
            

            return View(model);
        }

        private void GenerisiCmbStavke(NarucilacDodajVM model)
        {
            model.gradLista = db.Grad.Select(x => new SelectListItem
            {
                Text = x.Naziv,
                Value = x.GradId.ToString()
            }).ToList();

        }

        [Route("/ModulAdmin/Narucilac/Uredi")]
        public IActionResult Uredi(int id)
        {
            Narucilac n = db.Narucilac.Find(id);
            KorisnickiNalog kn = db.KorisnickiNalog.Where(w => w.Id == n.KorisnickiNalogId).SingleOrDefault();

            NarucilacDodajVM model = new NarucilacDodajVM();
            GenerisiCmbStavke(model);
            model.NarucilacId = id;
            model.ImePrezime = n.ImePrezime;
            model.GradId = n.GradId;
            model.KorisnickoIme = n.KorisnickiNalog.KorisnickoIme;
            model.Lozinka = n.KorisnickiNalog.Lozinka;

            model.Telefon = n.Telefon;
            model.Email = n.Email;
            model.Adresa = n.Adresa;
           
            GenerisiCmbStavke(model);
            return View(model);
        }

        [Route("/ModulAdmin/Narucilac/DodajSave")]
        [HttpPost]
        public IActionResult DodajSave(NarucilacDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Narucilac n = db.Narucilac.Find(model.NarucilacId);
                KorisnickiNalog kn = new KorisnickiNalog();

                if (n == null)
                {
                    n = new Narucilac();
 
                    db.Add(n);
                    db.Add(kn);

                }

                kn.KorisnickoIme = model.KorisnickoIme;
                kn.Lozinka = model.Lozinka;


                n.ImePrezime = model.ImePrezime;
                n.KorisnickiNalogId = kn.Id;
                n.Email = model.Email;
                n.Telefon = model.Telefon;
                n.Adresa = model.Adresa;
                n.GradId = model.GradId;

                db.SaveChanges();
                return RedirectToAction("List");

            }
            else
            {
                GenerisiCmbStavke(model);
                return View("Dodaj",model);

            }
        }
       

        [Route("/ModulAdmin/Narucilac/UrediSave")]
        [HttpPost]
        public IActionResult UrediSave(NarucilacDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Narucilac n = db.Narucilac.Find(model.NarucilacId);
                KorisnickiNalog kn = db.KorisnickiNalog.Where(w => w.Id == n.KorisnickiNalogId).FirstOrDefault();

                if (n == null)
                {
                    n = new Narucilac();
                    kn = new KorisnickiNalog();
                    db.Add(kn);
                    db.Add(n);

                }
                kn.KorisnickoIme = model.KorisnickoIme;
                kn.Lozinka = model.Lozinka;
                n.ImePrezime = model.ImePrezime;
                n.KorisnickiNalogId = kn.Id;
                n.Email = model.Email;
                n.Telefon = model.Telefon;
                n.Adresa = model.Adresa;
                n.GradId = model.GradId;

                db.SaveChanges();
                return RedirectToAction("List");

            }
            else
            {
                GenerisiCmbStavke(model);
                return View("Uredi", model);

            }
        }
    }
}