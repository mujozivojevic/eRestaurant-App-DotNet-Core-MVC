using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Areas.ModulAdmin.ViewModels.Dolasci;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Helper;

namespace WebApplication1.Areas.ModulAdmin.Controllers
{
    [Autorizacija(zaposlenik: true, korisnik: false)]

    [Area("ModulAdmin")]
    [Route("/ModulAdmin/Dolasci")]
    public class DolasciController : Controller
    {
        private MyContext _db;
        public DolasciController(MyContext baza)
        {
            _db = baza;
        }


        [Route("/ModulAdmin/Dolasci/List")]
        public IActionResult List()
        {
            DolasciList model = new DolasciList
            {
                htmlRows = _db.Dolasci.Select(s => new DolasciList.Row
                {
                    Id = s.Id,
                    Dolazak = s.Dolazak,
                    Odlazak = s.Odlazak,
                    Zaposlenik = s.Zaposlenik.Ime + " " + s.Zaposlenik.Prezime,
                    SatiRadili = s.SatiRadio
                }).ToList()
            };
            return View(model);
        }

        [Route("/ModulAdmin/Dolasci/Dodaj")]
        public IActionResult Dodaj()
        {
            DolasciDodajVM model = new DolasciDodajVM();
            GenerisiCmbStavke(model);

            return View(model);
        }

        [Route("/ModulAdmin/Dolasci/Zavrsio")]
        public IActionResult Zavrsio(int id)
        {
            Dolasci d = _db.Dolasci.Find(id);

            DateTime sada = DateTime.Now;
            d.Odlazak = sada;
            d.SatiRadio = sada.Subtract(d.Dolazak).Hours;

            _db.SaveChanges();
            return RedirectToAction("List");
        }


        [Route("/ModulAdmin/Dolasci/DodajSave")]
        public IActionResult DodajSave(DolasciDodajVM model)
        {

            if (ModelState.IsValid)
            {
                Dolasci d = new Dolasci();
                _db.Add(d);
                
                d.SatiRadio = 0;
                d.Dolazak = DateTime.Now;
                d.Odlazak = null;
                d.ZaposlenikId = model.ZaposlenikId;
                _db.SaveChanges();


                return RedirectToAction("List");
            }
            else
            {
                GenerisiCmbStavke(model);
                return View("Dodaj", model);
            }
        }

        private void GenerisiCmbStavke(DolasciDodajVM model)
        {
            model.ZaposleniciList = _db.Zaposlenik.Select(s => new SelectListItem
            {
                Text = s.Ime + " " + s.Prezime,
                Value = s.Id.ToString()
            }).ToList();
        }
    }
}