using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public class RezervacijaDodajVM
    {
        public List<SelectListItem> stolLista { get; set; }
        public List<SelectListItem> narucilacLista { get; set; }

        public Rezervacija Rezervacija { get; set; }
        public Stol StolMjesta { get; set; }
        public Narucilac Narucilac { get; set; }
        public DateTime DatumEvidencije { get; set; }
        public DateTime DatumRezervacije { get; set; }
    }
}
