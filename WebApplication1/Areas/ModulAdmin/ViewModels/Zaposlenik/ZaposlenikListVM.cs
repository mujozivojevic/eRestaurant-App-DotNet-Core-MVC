using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Zaposlenik
{
    public class ZaposlenikListVM
    {
        public List<Row> htmlRows { get; set; }
        public class Row
        {
            public int ZaposlenikId;
            public string ImePrezime { get; set; }
            public string JMBG { get; set; }
            public string DatumRodjenja { get; set; }
            public string Telefon { get; set; }
            public string Email { get; set; }
            public string Adresa { get; set; }

            public string KorisnickoIme { get; set; }
            public string Lozinka { get; set; }
            public string Grad { get; set; }
            public string RadnoMjesto { get; set; }
            

        }
    }
}
