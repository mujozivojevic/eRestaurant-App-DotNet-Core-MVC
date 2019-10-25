using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Dolasci
{
    public class DolasciList
    {
        public List<Row> htmlRows { get; set; }
        public class Row
        {
            public int Id { get; set; }
            public string Zaposlenik { get; set; }
            public DateTime? Dolazak { get; set; }
            public DateTime? Odlazak { get; set; }
            public int? SatiRadili { get; set; }

        }
    }
}
