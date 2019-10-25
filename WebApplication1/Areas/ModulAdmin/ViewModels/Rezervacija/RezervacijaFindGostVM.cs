using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Areas.ModulAdmin.Controllers;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Rezervacija
{
    public class RezervacijaFindGostVM
    {
        public List<Row> htmlRows { get; set; }
        public class Row
        {
            public string NarucilacId { get; set; }
            public string ImePrezime { get; set; }
            public string KorisnickoIme { get; set; }
            public string Email { get; set; }
            public string Telefon { get; set; }
            public string Adresa { get; set; }
            public string GradNaziv { get; set; }
        }
    }
}
