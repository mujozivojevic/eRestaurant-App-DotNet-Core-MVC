using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Jelo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Molimo unesite naziv")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Molimo unesite cijenu")]
        public float Cijena { get; set;  }
        public string Opis { get; set; }
        public string JedinicaMjere { get; set; }
        public string Slika { get; set; }

        public int VrstaJelaID { get; set; }
        [ForeignKey("VrstaJelaID")]
        public VrstaJela VrstaJela { get; set; }
    }
}
