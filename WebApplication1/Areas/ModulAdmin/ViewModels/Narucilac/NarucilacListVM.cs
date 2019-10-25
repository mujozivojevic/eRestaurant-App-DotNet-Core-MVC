using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Narucilac
{
   
    public class NarucilacListVM
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
