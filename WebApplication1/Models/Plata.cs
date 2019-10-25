using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Plata
    {
        public int Id { get; set; }
        public int ZaposlenikId { get; set; }
        [ForeignKey("ZaposlenikId")]
        public Zaposlenik Zaposlenik { get; set; }

        public float Iznos { get; set; }
        public DateTime Datum { get; set; }
    }
}
