using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.EF;
using WebApplication1.Models;
using WebApplication1.Areas.ModulAdmin.ViewModels.Jelo;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helper;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebApplication1.Areas.ModulAdmin.Controllers
{
    [Autorizacija(zaposlenik: true, korisnik: false)]

    [Area("ModulAdmin")]
    [Route("/ModulAdmin/Jelo")]
    public class JeloController : Controller
    {
        private MyContext db;
        private readonly IHostingEnvironment hostingEnvironment;
        public JeloController(MyContext baza, 
                              IHostingEnvironment hostingEnvironment)
        {
            
            db = baza;
            this.hostingEnvironment = hostingEnvironment;
        }


        [Route("/ModulAdmin/Jelo/List")]
        public IActionResult List()
        {

            JeloListVM model = new JeloListVM
            {
                htmlRows = db.Jelo.Select(j => new JeloListVM.Row()
                {
                    Id = j.Id,
                    Naziv = j.Naziv,
                    VrstaJela = j.VrstaJela.Naziv,
                    Opis = j.Opis,
                    Cijena = j.Cijena,
                    Slika = j.Slika,
                    Sastojci = db.ImaSastojke.Where(s => s.JeloId == j.Id).Select(q => new JeloListVM.SastojakVM
                    {
                        Naziv = q.Sastojci.Naziv,
                        Opis = q.Sastojci.Opis
                    }).ToList()

                }).ToList()
            };


            return View(model);
        }
        [Route("/ModulAdmin/Jelo/Obrisi")]
        [HttpGet]
        public IActionResult Obrisi(int id)
        {
            Jelo obrisati = db.Jelo.SingleOrDefault(j => j.Id == id);

            if (obrisati == null)
                return View("Error");

            db.Remove(obrisati);
            db.SaveChanges();
            TempData["porukaDelete"] = "Uspjesno obrisan korisnik";

            return RedirectToAction("List");
        }

        [Route("/ModulAdmin/Jelo/Dodaj")]
        public IActionResult Dodaj()
        {
            var model = new JeloDodajVM();
            GenerisiCmbStavke(model);


            return View(model);
        }

        [Route("/ModulAdmin/Jelo/DodajVrstuJela")]

        public IActionResult DodajVrstuJela()
        {
            var model = new JeloVrstaDodaj();

            return View(model);
        }
        [Route("/ModulAdmin/Jelo/DodajVrstuSave")]

        public IActionResult DodajVrstuSave(JeloVrstaDodaj model)
        {
            if (ModelState.IsValid)
            {
                VrstaJela vrsta = db.VrstaJela.Find(model.VrstaJelaId);

                if (vrsta == null)
                {
                    vrsta = new VrstaJela();
                    db.Add(vrsta);
                }

                vrsta.Naziv = model.Naziv;
                
                db.SaveChanges();

                return RedirectToAction("Dodaj");

            }
            else
            {
                return View("DodajVrstuJela", model);
            }
        }

        [Route("/ModulAdmin/Jelo/Uredi")]
        [HttpGet]

        public IActionResult Uredi(int id)
        {
            Jelo jelo = db.Jelo.Find(id);
            

            JeloDodajVM model = new JeloDodajVM();
            GenerisiCmbStavke(model);

            model.JeloId = id;
            model.Cijena = jelo.Cijena;
            model.Naziv = jelo.Naziv;
            model.Opis = jelo.Opis;
            model.VrstaJelaId = jelo.VrstaJelaID;


            IQueryable<ImaSastojke> imaSastojkes = db.ImaSastojke.Where(w => w.JeloId == id);

            foreach(ImaSastojke iS in imaSastojkes)
            {
                db.Remove(iS);
               
            }
            db.SaveChanges();
            return View(model);
        }

         private void GenerisiCmbStavke(JeloDodajVM model)
        {

            model.sastojciLista = db.Sastojci.Select(x => new SelectListItem
            {
                Text = x.Naziv,
                Value = x.Id.ToString()
            }).ToList();
            model.vrstaJelaLista = db.VrstaJela.Select(x => new SelectListItem
            {
                Text = x.Naziv,
                Value = x.Id.ToString()
            }).ToList();

        }

        
        [Route("/ModulAdmin/Jelo/DodajSave")]
        [HttpPost]
        public IActionResult DodajSave(JeloDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Jelo jelo = db.Jelo.Find(model.JeloId);

                if (jelo == null)
                {
                    jelo = new Jelo();
                    db.Add(jelo);
                }
               
                string uniqueFileName = null;
                if(model.Slika != null)
                {
                   string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                   uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Slika.FileName;
                   string filePath =  Path.Combine(uploadsFolder, uniqueFileName);
                    model.Slika.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                jelo.Cijena = model.Cijena;
                jelo.Naziv = model.Naziv;
                jelo.Opis = model.Opis;
                jelo.VrstaJelaID = model.VrstaJelaId;
                jelo.Slika = uniqueFileName;
                db.SaveChanges();



                Jelo x = db.Jelo.Where(y => y.Id == jelo.Id).SingleOrDefault();


                foreach (int s in model.sastojciOdabrani)
                {
                    ImaSastojke iS = new ImaSastojke
                    {
                        JeloId = x.Id,
                        SastojciId = s
                    };
                    db.Add(iS);
                }
                db.SaveChanges();
                return RedirectToAction("List");
           

            }
            else
            {
                GenerisiCmbStavke(model);
                return View("Dodaj",model);
            }
        }
        [Route("/ModulAdmin/Jelo/UrediSave")]
        [HttpPost]
        public IActionResult UrediSave(JeloDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Jelo jelo = db.Jelo.Find(model.JeloId);

                if (jelo == null)
                {
                    jelo = new Jelo();
                    db.Add(jelo);
                }
                string uniqueFileName = null;
                if (model.Slika != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Slika.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Slika.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                jelo.Cijena = model.Cijena;
                jelo.Naziv = model.Naziv;
                jelo.Opis = model.Opis;
                jelo.VrstaJelaID = model.VrstaJelaId;
                jelo.Slika = uniqueFileName;


                Jelo x = db.Jelo.Where(y => y.Id == jelo.Id).SingleOrDefault();


                foreach (int s in model.sastojciOdabrani)
                {
                    ImaSastojke iS = new ImaSastojke
                    {
                        JeloId = x.Id,
                        SastojciId = s
                    };
                    db.Add(iS);
                }
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