using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Areas.ModulAdmin.ViewModels.Obavijest;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Helper;

namespace WebApplication1.Areas.ModulAdmin.Controllers
{
    [Autorizacija(zaposlenik: true, korisnik: false)]

    [Area("ModulAdmin")]
    [Route("/ModulAdmin/Obavijest")]
    public class ObavijestController : Controller
    {
        private MyContext _db;
        public ObavijestController(MyContext baza)
        {
            _db = baza;
        }
        

        [Route("/ModulAdmin/Obavijest/")]
        [Route("/ModulAdmin/Obavijest/List")]
        public IActionResult List()
        {
            ObavijestListVM model = new ObavijestListVM
            {
                htmlRows = _db.Obavijest.Select(s => new ObavijestListVM.Row
                {
                    ObavijestId = s.Id,
                    Naziv = s.Naziv,
                    Sadrzaj = s.Sadrzaj,
                    Datum = s.Datum,
                    ImePrezime = s.Zaposlenik.Ime + " " + s.Zaposlenik.Prezime
                }).ToList()
            };         
            return View(model);
        }


        [Route("/AdminModul/Dodaj")]
        public IActionResult Dodaj()
        {
            ObavijestDodajVM model = new ObavijestDodajVM();
            GenerisiCmbStavke(model);
            return View(model);
        }
        [Route("/AdminModul/Obavijest/Obrisi")]
        [HttpGet]
        public IActionResult Obrisi(int id)
        {
            Obavijest obrisi = _db.Obavijest.Find(id);
            _db.Remove(obrisi);
            _db.SaveChanges();
            return RedirectToAction("List");
        }

        [Route("/AdminModul/Obavijest/DodajSave")]
        [HttpPost]
        public IActionResult DodajSave(ObavijestDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Obavijest o = _db.Obavijest.Find(model.ObavijestId);

                if (o == null)
                {
                    o = new Obavijest();
                    _db.Add(o);

                }

                o.Datum = model.Datum;
                o.Naziv = model.Naziv;
                o.Sadrzaj = model.Sadrzaj;
                o.ZaposlenikId = model.ZaposlenikId;

                _db.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                GenerisiCmbStavke(model);
                return View("Dodaj",model);

            }
        }

        [Route("/AdminModul/Obavijest/UrediSave")]
        [HttpPost]
        public IActionResult UrediSave(ObavijestDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Obavijest o = _db.Obavijest.Find(model.ObavijestId);

                if (o == null)
                {
                    o = new Obavijest();
                    _db.Add(o);

                }

                o.Datum = model.Datum;
                o.Naziv = model.Naziv;
                o.Sadrzaj = model.Sadrzaj;
                o.ZaposlenikId = model.ZaposlenikId;

                _db.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                GenerisiCmbStavke(model);
                return View("Uredi", model);

            }
        }
        private void GenerisiCmbStavke(ObavijestDodajVM model)
        {
            model.Zaposlenici = _db.Zaposlenik.Where(w => w.RadnoMjestoId == 1).Select(s => new SelectListItem
            {
                Text = s.Ime + " " + s.Prezime,
                Value = s.Id.ToString()
            }).ToList();
        }

        [Route("/AdminModul/Obavijest/Uredi")]
        [HttpGet]
        public IActionResult Uredi(int id)
        {
            Obavijest obavijest = _db.Obavijest.Find(id);

            ObavijestDodajVM model = new ObavijestDodajVM
            {
                ObavijestId = id,
                Datum = obavijest.Datum,
                Sadrzaj = obavijest.Sadrzaj,
                ZaposlenikId  = obavijest.ZaposlenikId,
                Naziv = obavijest.Naziv
            };
            GenerisiCmbStavke(model);
            return View(model);
        }

    }
}