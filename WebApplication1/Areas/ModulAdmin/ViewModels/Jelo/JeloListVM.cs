using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Jelo
{
    public class JeloListVM
    {
        public class SastojakVM
        {
            public string Naziv { get; set; }
            public string Opis { get; set; }

        }
        public class Row
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public float Cijena { get; set; }
            public string Opis { get; set; }

            public string VrstaJela { get; set; }

            public List<SastojakVM> Sastojci { get; set; }
            public string Slika { get; set; }
        }
        public List<Row> htmlRows { get; set; }
    }
}
