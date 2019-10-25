using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EF;
using WebApplication1.Helper;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels.Login;

namespace WebApplication1.Controllers
{
    public class AutentifikacijaController : Controller
    {

        private readonly MyContext _db;

        public AutentifikacijaController(MyContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(new LoginVM()
            {
                ZapamtiPassword = true
            });
           
        }

        public IActionResult Login(LoginVM input)
        {
            KorisnickiNalog korisnik = _db.KorisnickiNalog.
                       SingleOrDefault(x => x.KorisnickoIme == input.KorisnickoIme && x.Lozinka == input.Lozinka);


           


            if (korisnik == null)
            {
                TempData["error_poruka"] = "pogrešan username ili password";
                return View("Index", input);
            }
            HttpContext.SetLogiraniKorisnik(korisnik);
            Zaposlenik z = _db.Zaposlenik.Where(w => w.KorisnickiNalogId == korisnik.Id && (w.RadnoMjestoId == 1 || w.RadnoMjestoId == 2)).FirstOrDefault();

            if (z != null)
            {
      
                return Redirect("~/ModulAdmin/Home/Index");
            }
           
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {

            return RedirectToAction("Index");
        }
    }
}