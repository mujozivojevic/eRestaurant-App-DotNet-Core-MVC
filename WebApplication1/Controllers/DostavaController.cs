using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EF;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DostavaController : Controller
    {
        private MyContext db;
        public DostavaController(MyContext baza)
        {
            db = baza;
        }


        [HttpGet]
        public IActionResult Index()
        {

            ViewData["narudzba"] = db.Narudzba.ToList();

            ViewData["statusDostave"] = db.StatusDostave.ToList();

            ViewData["dostava"] = db.Dostava.ToList();

            ViewData["narucilac"] = db.Narucilac.ToList();
            return View();
        }
        public ActionResult Uredi(int Id)
        {

            Dostava j = db.Dostava.Where(x => x.Id == Id).SingleOrDefault();

            if (j != null)
            {
                ViewData["narudzba"] = db.Narudzba.ToList();
                ViewData["statusDostave"] = db.StatusDostave.ToList();
                ViewData["dostava"] = db.Dostava.ToList();
                ViewData["narucilac"] = db.Narucilac.ToList();
                return View(j);
            }

            return RedirectToAction("Index", "Index");
        }
        [HttpPost]
        public ActionResult Uredi(int Id, int StatusDostaveId, int NarudzbaId, DateTime DatumSlanja,
            DateTime DatumDostave)
        {

            Dostava j = db.Dostava.Where(x => x.Id == Id).SingleOrDefault();

            if (j != null)
            {
                j.DatumVrijemeSlanja = DatumSlanja;
                j.DatumVrijemeDostave = DatumDostave;               

                j.StatusDostaveId = StatusDostaveId;
                j.NarudzbaId = NarudzbaId;

                db.SaveChanges();

                ViewData["narudzba"] = db.Narudzba.ToList();
                ViewData["statusDostave"] = db.StatusDostave.ToList();
                ViewData["dostava"] = db.Dostava.ToList();
                ViewData["narucilac"] = db.Narucilac.ToList();

                return RedirectToAction("Uredi", "Dostava", new { Id = Id });
                /// 
              //  return RedirectToAction("Index", "test");
            }

            return RedirectToAction("Index", "Index");
        }
        [HttpPost]
        public IActionResult Index(int NarudzbaId, int StatusDostaveId, DateTime DatumVrijemeSlanja,
            DateTime DatumVrijemeDostave)
        {

            Dostava j = new Dostava();
            j.DatumVrijemeSlanja = DatumVrijemeSlanja;
            j.DatumVrijemeDostave = DatumVrijemeDostave;


            j.NarudzbaId = NarudzbaId;
            j.StatusDostaveId = StatusDostaveId;

            db.Dostava.Add(j);
            db.SaveChanges();


            ViewData["narudzba"] = db.Narudzba.ToList();
            ViewData["statusDostave"] = db.StatusDostave.ToList();
            ViewData["dostava"] = db.Dostava.ToList();
            ViewData["narucilac"] = db.Narucilac.ToList();
            return View();
        }

        public IActionResult Prikazi()
        {

            ViewData["narudzba"] = db.Narudzba.ToList();

            ViewData["statusDostave"] = db.StatusDostave.ToList();

            ViewData["dostava"] = db.Dostava.ToList();

            ViewData["narucilac"] = db.Narucilac.ToList();

            return View();
        }
    }
}