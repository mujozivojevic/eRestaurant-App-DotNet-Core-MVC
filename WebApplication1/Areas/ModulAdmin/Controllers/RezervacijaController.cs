using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Areas.ModulAdmin.ViewModels.Rezervacija;
using WebApplication1.Helper;

namespace WebApplication1.Areas.ModulAdmin.Controllers
{
    [Autorizacija(zaposlenik: true, korisnik: false)]

    [Area("ModulAdmin")]
    [Route("/ModulAdmin/Rezervacija")]
    public class RezervacijaController : Controller
    {
        private MyContext db;
        public RezervacijaController(MyContext baza) { db = baza; }

        [Route("/ModulAdmin/Rezervacija/List")]
        public IActionResult List()
        {
            RezervacijeListVM model = new RezervacijeListVM
            {
                htmlRows = db.Rezervacija.Select(r => new RezervacijeListVM.Row()
                {
                    RezervacijaId = r.Id,
                    DatumEvidencije = r.DatumEvidencije,
                    DatumRezervacije = r.DatumRezervacije,
                    ImePrezime = r.Narucilac.ImePrezime,
                    Naziv = r.Naziv,
                    BrojMjesta = db.RezervacijaStavke.Where(w=> w.RezervacijaId == r.Id).Select(g => g.Stol.BrojMjesta).FirstOrDefault()
                }).ToList()
          };
          return View(model);
        }


        [Route("/ModulAdmin/Rezervacija/DodajGost")]
        public IActionResult DodajGost()
        {
            RezervacijaFindGostVM model = new RezervacijaFindGostVM
            {
                htmlRows = db.Narucilac.Select(n => new RezervacijaFindGostVM.Row
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

   

        [Route("/ModulAdmin/Rezervacija/Dodaj")]
        public IActionResult Dodaj(int id)
        {
            RezervacijaDodajVM model = new RezervacijaDodajVM();
         
            Narucilac n = db.Narucilac.Find(id);
            model.NarucilacId = id;
            model.NarucilacIme = n.ImePrezime;
            model.DatumEvidencije = DateTime.Now;
            return View(model);
        }


        [Route("/ModulAdmin/Rezervacija/Uredi")]
        [HttpGet]
        public IActionResult Uredi(int id)
        {
            Rezervacija rezervacija = db.Rezervacija.Find(id);
            RezervacijaStavke rs = db.RezervacijaStavke.Where(w => w.RezervacijaId == id).FirstOrDefault();
            Narucilac n = db.Narucilac.Where(w => w.NarucilacId == rezervacija.NarucilacId).FirstOrDefault();
            RezervacijaDodajVM model = new RezervacijaDodajVM();

            model.RezervacijaId = id;
            model.Naziv = rezervacija.Naziv;
            model.DatumEvidencije = rezervacija.DatumEvidencije;
            model.DatumRezervacije = rezervacija.DatumRezervacije;
            model.BrojStolova = rs.StolId;
            model.NarucilacIme = n.ImePrezime;
            model.NarucilacId = rezervacija.NarucilacId;
             

            return View(model);
        }

        [Route("/ModulAdmin/Rezervacija/Obrisi")]
        [HttpGet]
        public IActionResult Obrisi(int id)
        {
            Rezervacija obrisati = db.Rezervacija.SingleOrDefault(r => r.Id == id);
            IEnumerable<RezervacijaStavke> obrisatiStavke = db.RezervacijaStavke.Where(rs => rs.RezervacijaId == id);
            if (obrisati == null)
                return View("Error");

            foreach (RezervacijaStavke rezervacijaStavke in obrisatiStavke)
            {
                db.Remove(rezervacijaStavke);
            }
            db.Remove(obrisati);
            db.SaveChanges();

            return RedirectToAction("List");
        }
        [Route("/ModulAdmin/Rezervacija/DodajSave")]
        [HttpPost]
        public IActionResult DodajSave(RezervacijaDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Rezervacija r = db.Rezervacija.Find(model.RezervacijaId);
                RezervacijaStavke rs = db.RezervacijaStavke.Where(w => w.RezervacijaId == model.RezervacijaId).FirstOrDefault();
                if (r == null)
                {
                    r = new Rezervacija();
                    rs = new RezervacijaStavke();
                    db.Add(r);
                    db.Add(rs);
                }


                r.NarucilacId = model.NarucilacId;
                r.Naziv = model.Naziv;
                r.DatumEvidencije = model.DatumEvidencije;
                r.DatumRezervacije = model.DatumRezervacije;

                rs.RezervacijaId = r.Id;
                rs.StolId = model.BrojStolova;

                db.SaveChanges();

                return RedirectToAction("List");
            }
            else
            {
             
                return View("Dodaj", model);
            }
    
        }

        [Route("/ModulAdmin/Rezervacija/UrediSave")]
        [HttpPost]
        public IActionResult UrediSave(RezervacijaDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Rezervacija r = db.Rezervacija.Find(model.RezervacijaId);
                RezervacijaStavke rs = db.RezervacijaStavke.Where(w => w.RezervacijaId == model.RezervacijaId).FirstOrDefault();
                if (r == null)
                {
                    r = new Rezervacija();
                    rs = new RezervacijaStavke();
                    db.Add(r);
                    db.Add(rs);
                }


                r.NarucilacId = model.NarucilacId;
                r.Naziv = model.Naziv;
                r.DatumEvidencije = model.DatumEvidencije;
                r.DatumRezervacije = model.DatumRezervacije;

                rs.RezervacijaId = r.Id;
                rs.StolId = model.BrojStolova;

                db.SaveChanges();

                return RedirectToAction("List");
            }
            else
            {
                return View("Uredi", model);
            }

        }
    

      
     
    }
}