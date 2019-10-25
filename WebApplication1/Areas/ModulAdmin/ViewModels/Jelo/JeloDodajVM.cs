using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Jelo
{
    public class JeloDodajVM
    {

        public int JeloId { get; set; }
        public int SastojciId { get; set; }
        public List<SelectListItem> sastojciLista { get; set; }

        public List<int> sastojciOdabrani { get; set; }

        [BindRequired]
        public int VrstaJelaId { get; set; }
    
        public List<SelectListItem> vrstaJelaLista { get; set; }

        [Required(ErrorMessage="Naziv jela je obavezno polje")]
        [RegularExpression("^.{4,}$", ErrorMessage = "Opis mora imat više od 4 slova")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Opis je obavezno polje")]
        [RegularExpression("^.{6,}$", ErrorMessage = "Opis mora imat više od 6 slova")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Cijena je obavezno polje")]
        [RegularExpression("^(?(?=99)99(\\.0+)?|([1-9]\\d?(\\.\\d+)?))$", ErrorMessage = "Cijena mora biti veća od 0 i manja od 100")]
        public float Cijena { get; set; }

        public IFormFile Slika { get; set; }
       
    }
}
