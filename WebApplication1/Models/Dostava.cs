using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Dostava
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumVrijemeSlanja { get; set; }
        public DateTime? DatumVrijemeDostave { get; set; }

        [ForeignKey("NarudzbaId")]
        public Narudzba Narudzba { get; set; }
        public int NarudzbaId { get; set; }

        [ForeignKey("StatusDostaveId")]
        public StatusDostave StatusDostave { get; set; }
        public int StatusDostaveId { get; set; }

    }
}
