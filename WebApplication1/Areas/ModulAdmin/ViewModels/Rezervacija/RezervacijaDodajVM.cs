using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Rezervacija
{
    public class RezervacijaDodajVM
    {

        [Required(ErrorMessage = "Naziv događaja je obavezno polje")]
        [RegularExpression("^.{4,}$", ErrorMessage = "Događaj mora imat više od 4 slova")]
        public string Naziv { get; set; }

        public int RezervacijaId { get; set; }



      
        public int NarucilacId { get; set; }

        public int BrojStolova { get; set; }


        public string NarucilacIme { get; set; }
        public DateTime DatumEvidencije { get; set; }
        public DateTime DatumRezervacije { get; set; }
    }
}
