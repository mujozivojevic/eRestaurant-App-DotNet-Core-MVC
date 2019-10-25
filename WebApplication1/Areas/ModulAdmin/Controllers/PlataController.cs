using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Areas.ModulAdmin.ViewModels.Plata;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Helper;

namespace WebApplication1.Areas.ModulAdmin.Controllers
{
    [Autorizacija(zaposlenik: true, korisnik: false)]

    [Area("ModulAdmin")]
    [Route("/ModulAdmin/Plata")]
    public class PlataController : Controller
    {
        private MyContext _db;
        public PlataController(MyContext baza)
        {
            _db = baza;
        }
        [Route("/ModulAdmin/Plata")]
        [Route("/ModulAdmin/Plata/List")]
        public IActionResult List()
        {
            PlataListVM model = new PlataListVM
            {
                htmlRows = _db.Plata.Select(s => new PlataListVM.Row
                { 
                    ZaposlenikId = s.ZaposlenikId,
                    DatumMjesec = s.Datum.Month,
                    PlataId = s.Id,
                    Datum = s.Datum,
                    Iznos = s.Iznos,
                    ZaposlenikIme = s.Zaposlenik.Ime + " " + s.Zaposlenik.Prezime
                }).ToList()
            };
            return View(model);
        }

        [Route("/ModulAdmin/Plata/ZaposlenikList")]
        public IActionResult Zaposlenik(int id)
        {
            PlataListVM model = new PlataListVM
            {
                htmlRows = _db.Plata.Where(w=>w.ZaposlenikId == id).Select(s => new PlataListVM.Row
                {
                    ZaposlenikId = s.ZaposlenikId,
                    DatumMjesec = s.Datum.Month,
                    PlataId = s.Id,
                    Datum = s.Datum,
                    Iznos = s.Iznos,
                    ZaposlenikIme = s.Zaposlenik.Ime + " " + s.Zaposlenik.Prezime
                }).ToList()
            };
            return View(model);
        }


        [Route("/ModulAdmin/Plata/Dodaj")]
        public IActionResult Dodaj()
        {
            PlataDodajVM model = new PlataDodajVM();
            GenerisiCmbStavke(model);
            return View(model);
        }

        private void GenerisiCmbStavke(PlataDodajVM model)
        {
            model.Zaposlenici = _db.Zaposlenik.Select(s => new SelectListItem
            {
                Text = s.Ime + " " + s.Prezime,
                Value = s.Id.ToString()
            }).ToList();
        }
        [Route("/AdminModul/Plata/Obrisi")]
        [HttpGet]
        public IActionResult Obrisi(int id)
        {
            Plata obrisi = _db.Plata.Find(id);
            _db.Remove(obrisi);
            _db.SaveChanges();
            return RedirectToAction("List");
        }
        [Route("/ModulAdmin/Plata/Uredi")]
        [HttpGet]
        public IActionResult Uredi(int id)
        {
            Plata p = _db.Plata.Find(id);

            PlataDodajVM model = new PlataDodajVM();
            model.Iznos = p.Iznos;
            model.ZaposlenikId = p.ZaposlenikId;
            GenerisiCmbStavke(model);
            return View(model);
        }


        [Route("/ModulAdmin/Plata/DodajSave")]
        [HttpPost]
        public IActionResult DodajSave(PlataDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Plata p = _db.Plata.Find(model.PlataId);

                if (p == null)
                {
                    p = new Plata
                    {
                        Datum = DateTime.Now
                    };
                    _db.Add(p);
                }
                p.Iznos = model.Iznos;
                p.ZaposlenikId = model.ZaposlenikId;
                _db.SaveChanges();

                return RedirectToAction("List");
            }
            else
            {
                GenerisiCmbStavke(model);
                return View("Dodaj",model);

            }
        }

        [Route("/ModulAdmin/Plata/UrediSave")]
        [HttpPost]
        public IActionResult UrediSave(PlataDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Plata p = _db.Plata.Find(model.PlataId);

                if (p == null)
                {
                    p = new Plata
                    {
                        Datum = DateTime.Now
                    };
                    _db.Add(p);
                }
                p.Iznos = model.Iznos;
                p.ZaposlenikId = model.ZaposlenikId;
                _db.SaveChanges();

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