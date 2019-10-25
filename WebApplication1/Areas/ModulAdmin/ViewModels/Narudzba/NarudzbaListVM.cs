using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Narudzba
{
    public class NarudzbaListVM
    {

        public List<Row> htmlRows { get; set; }
        public int NarudzbaId { get; set; }

        public class Row
        {
            public int NarudzbaId { get;  set; }
            public DateTime VrijemeDostave { get; set; }
            public DateTime DatumNarudzbe { get; set; }
            public string ImePrezime { get; set; }
            public int StatusDostaveId { get; set; }
            public string StatusDostave { get;  set; }
            public string Adresa { get;  set; }
            public string Telefon { get; set; }
        }
    }
}
