using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Obavijest
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Sadrzaj { get; set; }

        public int ZaposlenikId { get; set; }
        [ForeignKey("ZaposlenikId")]
        public Zaposlenik Zaposlenik { get; set; }
        public DateTime Datum { get; set; }
    }
}
