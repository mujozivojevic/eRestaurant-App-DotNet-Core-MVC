using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Rezervacija
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumEvidencije { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public string Naziv { get; set; }
        public int NarucilacId { get; set; }
        [ForeignKey("NarucilacId")]
        public Narucilac Narucilac { get; set; }
    }
}
