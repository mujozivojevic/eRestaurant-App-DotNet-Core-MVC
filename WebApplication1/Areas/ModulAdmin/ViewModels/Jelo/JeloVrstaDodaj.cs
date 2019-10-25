using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Jelo
{
    public class JeloVrstaDodaj
    {
        public int VrstaJelaId { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [RegularExpression("^.{2,}$", ErrorMessage = "Naziv mora imat više od 2 karaktera")]
        public string Naziv { get; set; }
    }
}
