using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Obavijest
{

    public class ObavijestDodajVM
    {
        public int ObavijestId { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje")]
        [RegularExpression("^.{3,}$", ErrorMessage = "Naziv imat više od 3 karaktera")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Sadržaj je obavezno polje")]
        [RegularExpression("^.{10,}$", ErrorMessage = "Sadržaj mora imati više od 10 karaktera")]
        public string Sadrzaj { get; set; }
        public DateTime Datum { get; set; }

        [BindRequired]
        public int ZaposlenikId { get; set; }
        public List<SelectListItem> Zaposlenici { get; set; }


    }
}
