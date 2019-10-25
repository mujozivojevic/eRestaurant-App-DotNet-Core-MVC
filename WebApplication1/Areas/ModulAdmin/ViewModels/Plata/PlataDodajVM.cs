using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Plata
{
    public class PlataDodajVM
    {
        public int PlataId { get; set; }
        public int ZaposlenikId { get; set; }
        public List<SelectListItem> Zaposlenici { get; set; }

        public int DatumMjesec { get; set; }
        public float Iznos { get; set; }
        public DateTime Datum { get; set; }

    }
}
