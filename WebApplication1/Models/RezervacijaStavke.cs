using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class RezervacijaStavke
    {
        [Key]
        public int Id { get; set; }

        public int RezervacijaId { get; set; }
        [ForeignKey("RezervacijaId")]
        public Rezervacija Rezervacija { get; set; }

        public int StolId { get; set; }
        [ForeignKey("StolId")]
        public Stol Stol { get; set; }


    }
}
