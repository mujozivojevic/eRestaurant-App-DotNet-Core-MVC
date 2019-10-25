using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EF;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class NarudzbaController : Controller
    {
        private MyContext db;
        public NarudzbaController(MyContext baza)
        {
            db = baza;
        }
        [HttpGet]
        public IActionResult Index()
        {

            ViewData["narucilac"] = db.Narucilac.ToList();

            ViewData["narudzba"] = db.Narudzba.ToList();         

            return View();
        }

        [HttpPost]
        public IActionResult Index(int NarucilacId, DateTime DatumNarudzbe )
        {

            Narudzba j = new Narudzba();
            j.DatumNarudzbe = DatumNarudzbe;
           

            j.NarucilacId = NarucilacId;
         
            db.Narudzba.Add(j);
            db.SaveChanges();


            ViewData["narucilac"] = db.Narucilac.ToList();
            ViewData["narudzba"] = db.Narudzba.ToList();

            return View();
        }
        public ActionResult Uredi(int Id)
        {

            Narudzba j = db.Narudzba.Where(x => x.Id == Id).SingleOrDefault();

            if (j != null)
            {
                ViewData["narucilac"] = db.Narucilac.ToList();
                ViewData["narudzba"] = db.Narudzba.ToList();
                return View(j);
            }

            return RedirectToAction("Index", "Index");
        }

        [HttpPost]
        public ActionResult Uredi(int Id, int NarucilacId, DateTime DatumNarudzbe)
        {

            Narudzba j = db.Narudzba.Where(x => x.Id == Id).SingleOrDefault();

            if (j != null)
            {
                j.DatumNarudzbe = DatumNarudzbe;
                
                j.NarucilacId = NarucilacId;               

                db.SaveChanges();

                ViewData["narucilac"] = db.Narucilac.ToList();
                ViewData["narudzba"] = db.Narudzba.ToList();

                return RedirectToAction("Uredi", "Narudzba", new { Id = Id });
                /// 
              //  return RedirectToAction("Index", "test");
            }

            return RedirectToAction("Index", "Index");
        }
        [HttpGet]
        public IActionResult Prikazi()
        {

            ViewData["narucilac"] = db.Narucilac.ToList();

            ViewData["narudzba"] = db.Narudzba.ToList();
        

            return View();
        }
    }
}