using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Dolasci
    {
        [Key]
        public int Id { get; set; }
        
        public int ZaposlenikId { get; set; }
        [ForeignKey("ZaposlenikId")]
        public Zaposlenik Zaposlenik { get; set; }


        public DateTime Dolazak { get; set; }
        public DateTime? Odlazak { get; set; }

        public int? SatiRadio { get; set; }

    }
}
