using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Plata
{
    public class PlataListVM
    {
        public List<Row> htmlRows { get; set; }

        public class Row
        {
            public int Id { get; set; }
            public int ZaposlenikId { get; set; }
            public string ZaposlenikIme { get; set; }
            public int DatumMjesec { get; set; }
            public float Iznos { get; set; }
            public DateTime Datum { get; set; }
            public int PlataId { get;  set; }
        }
    }
}
