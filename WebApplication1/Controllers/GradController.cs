using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EF;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GradController: Controller
    {
        private MyContext db;
        public GradController(MyContext baza)
        {
            db = baza;
        }
        public IActionResult Prikazi()
        {
            List<Grad> gradovi = db.Grad.ToList();
            ViewData["gradovi-kljuc"] = gradovi;
            return View();
        }

        public IActionResult Obrisi(int GradId)
        {
     
            Grad g = db.Grad.Find(GradId);
            if (g==null)
            {
                return Content("Grad ne postoji.");
            }
            db.Remove(g);

            db.SaveChanges();
            return Redirect("/Grad/Prikazi");
        }

        public IActionResult DodajGrad(string gradNaziv, string gradPostanskiBroj)
        {

            Grad x = new Grad();
            // ViewData["grad-kljuc"] = x;
            x.Naziv =gradNaziv;
            x.PostanskiBroj = int.Parse(gradPostanskiBroj);
            
            db.Add(x);
            db.SaveChanges();
            return Redirect("/Grad/Prikazi");
        }
    }
}
