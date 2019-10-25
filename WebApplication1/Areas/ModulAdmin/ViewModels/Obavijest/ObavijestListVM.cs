using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Obavijest
{
    public class ObavijestListVM
    {

        public List<Row> htmlRows {get;set;}
        public class Row
        {
            public string Naziv { get; set; }
            public string Sadrzaj { get; set; }
            public string ImePrezime { get; set; }
            public DateTime Datum { get; set; }
            public int ObavijestId { get; set; }
        }
    }
}
