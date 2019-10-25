using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Rezervacija
{
    public class RezervacijeListVM
    {
        public List<Row> htmlRows { get; set; }
        public class Row
        {
            public int RezervacijaId { get; set; }
            public DateTime DatumEvidencije { get; set; }
            public DateTime DatumRezervacije { get; set; }
            public string Naziv { get; set; }
           
            public string ImePrezime { get; set; }

            public int BrojMjesta { get; set; }
        }
    }
}
