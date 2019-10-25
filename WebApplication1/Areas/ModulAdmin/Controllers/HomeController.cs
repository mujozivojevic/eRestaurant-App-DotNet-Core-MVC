using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EF;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Areas.ModulAdmin.Controllers
{
    [Autorizacija(zaposlenik: true, korisnik: false)]
    [Area("ModulAdmin")]
    [Route("/ModulAdmin/Home")]
    public class HomeController : Controller
    {
        private readonly MyContext _db;

        public HomeController(MyContext db)
        {
            _db = db;
        }
        [Route("/ModulAdmin/Home/Index")]
        public IActionResult Index()
        {
            //KorisnickiNalog korisnik = HttpContext.GetLogiraniKorisnik();
            //if (korisnik == null)
            //{
            //    TempData["error_poruka"] = "Nemate pravo pristupa";
            //    return Redirect("/Autentifikacija/Index");
            //}
            //else
            //{
            //    Zaposlenik z = _db.Zaposlenik.Where(w => w.KorisnickiNalogId == korisnik.Id && (w.RadnoMjestoId == 1 || w.RadnoMjestoId == 2)).FirstOrDefault();
            //    if (z == null)
            //    {
            //        TempData["error_poruka"] = "Nemate pravo pristupa";
            //        return Redirect("/Autentifikacija/Index");
            //    }

            //}
            return View();
        }
    }
}