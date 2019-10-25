using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Narudzba
{
    public class NarudzbaDetaljiVM
    {
        public string ImePrezime { get; set; }
        public class JeloVM
        {
            public string Naziv { get; set; }
            public float Cijena { get; set; }
        }
   
        public List<Row> htmlRows { get; set; }
        public class Row
        {
   
            public float Cijena { get; set; }

            public List<JeloVM> Jela { get; set; }
            public int Kolicina { get; set; }
            public float Ukupno { get; set; }
        }
    }
}
