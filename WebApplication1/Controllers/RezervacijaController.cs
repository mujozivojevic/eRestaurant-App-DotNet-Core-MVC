using System;
using System.Collections;
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
    public class RezervacijaController : Controller
    {
        private MyContext db;
        public RezervacijaController(MyContext baza) { db = baza; }


        private RezervacijaDodajVM GenerisiCmbStavke()
        {
            return new RezervacijaDodajVM
            {
                stolLista = db.Stol.Select(x => new SelectListItem
                {
                    Text = x.Id.ToString(),
                    Value = x.Id.ToString()
                }).ToList(),
                narucilacLista = db.Narucilac.Select(n => new SelectListItem
                {
                    Text = n.ImePrezime,
                    Value = n.NarucilacId.ToString()
                }).ToList(),
            };
        }
     

        
        public IActionResult Prikazi()
        {
            RezervacijaPrikaziVM model = new RezervacijaPrikaziVM
            {

                htmlRows = db.RezervacijaStavke.Select(rs => new RezervacijaPrikaziVM.Row()
                {
                    Id = rs.RezervacijaId,
                    DatumEvidencije = DateTime.Now,
                    DatumRezervacije = rs.Rezervacija.DatumRezervacije,
                    Narucilac = rs.Rezervacija.Narucilac,
                    Stolovi = db.Stol.Where(x=> x.Id == rs.StolId).Select(q=> new RezervacijaPrikaziVM.StolVM
                    {
                        BrojStola = q.Id,
                        BrojMjesta = q.BrojMjesta
                    }).FirstOrDefault()
                }).ToList()
          };

           return View("Prikazi",model);
        }

        public IActionResult DodajForm()
        {
            RezervacijaDodajVM model = this.GenerisiCmbStavke();

            return View("DodajForm", model);
        }
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
            TempData["porukaDelete"] = "Uspjesno obrisana rezervacija";

            return Redirect("/Rezervacija/Prikazi");
        }

        public IActionResult DodajSave(RezervacijaDodajVM model)
        {
            Rezervacija r = new Rezervacija();

            r.DatumEvidencije = DateTime.Now;
            r.DatumRezervacije = model.DatumRezervacije;
            r.NarucilacId = model.Narucilac.NarucilacId;

            db.Add(r);
            db.SaveChanges();

            RezervacijaStavke rs = new RezervacijaStavke();

            rs.StolId = model.StolMjesta.Id;
            rs.RezervacijaId = db.Rezervacija.Where(x=> x.Id == r.Id).FirstOrDefault().Id;

            db.Add(rs);
            db.SaveChanges();

            TempData["porukaDodan"] = "Uspjesno dodana rezervacija";
            return Redirect("/Rezervacija/Prikazi");
        }
    }
}