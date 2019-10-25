using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Areas.ModulAdmin.ViewModels.Narudzba;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helper;

namespace WebApplication1.Areas.ModulAdmin.Controllers
{
    [Autorizacija(zaposlenik: true, korisnik: false)]

    [Area("ModulAdmin")]
    [Route("/ModulAdmin/Zaposlenik")]
    public class NarudzbaController : Controller
    {

        private MyContext db;
        public NarudzbaController(MyContext baza)
        {
            db = baza;
        }

        [Route("/ModulAdmin/Narudzba/List")]
        public IActionResult List()
        {
            NarudzbaListVM model = new NarudzbaListVM
            {
                htmlRows = db.Narudzba.Select(s => new NarudzbaListVM.Row
                {
                    NarudzbaId = s.Id,
                    ImePrezime = s.Narucilac.ImePrezime,
                    Adresa = s.Narucilac.Adresa,
                    Telefon = s.Narucilac.Telefon,
                    DatumNarudzbe = s.DatumNarudzbe,
                    VrijemeDostave = db.Dostava.Where(w => w.NarudzbaId == s.Id).Select(x => x.DatumVrijemeSlanja).FirstOrDefault(),
                    StatusDostaveId = db.Dostava.Where(w => w.NarudzbaId == s.Id)
                                                   .Select(sd => sd.StatusDostave.Id).SingleOrDefault(),
                    StatusDostave = db.Dostava.Where(w=> w.NarudzbaId == s.Id)
                                              .Select(sd => sd.StatusDostave.Opis).SingleOrDefault()
                                                                               


                }).ToList()
            };

            return View(model);
        }
        [Route("/ModulAdmin/Narudzba/Detalji")]
        [HttpGet]
        public IActionResult Detalji(int id)
        {
           
            NarudzbaDetaljiVM model = new NarudzbaDetaljiVM
            {
                ImePrezime = db.Narudzba.Where(w => w.Id == id).Select(s => s.Narucilac.ImePrezime).SingleOrDefault(),
                htmlRows = db.NarudzbaStavke.Where(w => w.NarudzbaId == id).Select(s => new NarudzbaDetaljiVM.Row
                {
                    Kolicina = s.Kolicina,
                    Ukupno = db.Jelo.Where(w => w.Id == s.JeloId).Select(g => g.Cijena).FirstOrDefault() * s.Kolicina,
                    Jela = db.Jelo.Where(w => w.Id == s.JeloId).Select(g => new NarudzbaDetaljiVM.JeloVM
                    {
                        Naziv = g.Naziv,
                        Cijena = g.Cijena,
                    }).ToList()
                }).ToList()
            };
            return PartialView(model);
        }

        [Route("/ModulAdmin/Narudzba/Obrisi")]
        [HttpGet]
        public IActionResult Obrisi(int id)
        {
            Narudzba obrisi = db.Narudzba.Find(id);
            List<NarudzbaStavke> obrisi2 = db.NarudzbaStavke.Where(w=> w.NarudzbaId == id).ToList();
            Dostava obrisi3 = db.Dostava.Where(w=> w.NarudzbaId == id).FirstOrDefault();

            db.Remove(obrisi3);

            foreach (var item in obrisi2)
            {
                db.Remove(item);
            }
            db.Remove(obrisi);

            db.SaveChanges();

            return RedirectToAction("List");
        }

        [Route("/ModulAdmin/Narudzba/Dostavljeno")]
        public IActionResult Dostavljeno(int id)
        {
            Dostava dostava = db.Dostava.Where(w => w.NarudzbaId == id).SingleOrDefault();
            dostava.StatusDostaveId = 1;
            dostava.DatumVrijemeDostave = DateTime.Now;
            db.SaveChanges();

            return RedirectToAction("List");
        }
        [Route("/ModulAdmin/Narudzba/Cekanje")]

        public IActionResult Cekanje(int id)
        {
            Dostava dostava = db.Dostava.Where(w => w.NarudzbaId == id).SingleOrDefault();
            dostava.StatusDostaveId = 2;
            dostava.DatumVrijemeDostave = null;
            db.SaveChanges();
            return RedirectToAction("List");
        }

       
    }
}