using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class NarudzbaStavke
    {
        [ForeignKey("NarudzbaId")]
        public Narudzba Narudzba { get; set; }
        public int NarudzbaId { get; set; }

        [ForeignKey("JeloId")]

        public Jelo Jelo { get; set; }
        public int JeloId { get; set; }


        public float Cijena { get; set; }

        public int Kolicina { get; set; }


    }
}
