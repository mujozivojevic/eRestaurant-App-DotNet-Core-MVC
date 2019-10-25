using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EF;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StatusDostaveController : Controller
    {
        private MyContext db;
        public StatusDostaveController(MyContext baza)
        {
            db = baza;
        }
        [HttpGet]
        public IActionResult Index()
        {

            ViewData["statusDostave"] = db.StatusDostave.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Index(string Opis)
        {

            StatusDostave j = new StatusDostave();
            j.Opis = Opis;
           

            db.StatusDostave.Add(j);
            db.SaveChanges();


            ViewData["statusDostave"] = db.StatusDostave.ToList();
            

            return View();
        }

        [HttpGet]
        public IActionResult Prikazi()
        {
        
            ViewData["statusDostave"] = db.StatusDostave.ToList();

            return View();
        }
    }
}