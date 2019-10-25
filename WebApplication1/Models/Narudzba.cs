using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Narudzba
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumNarudzbe { get; set; }

        [ForeignKey("NarucilacId")]
        public Narucilac Narucilac { get; set; }
        public int NarucilacId { get; set; }
    }
}
