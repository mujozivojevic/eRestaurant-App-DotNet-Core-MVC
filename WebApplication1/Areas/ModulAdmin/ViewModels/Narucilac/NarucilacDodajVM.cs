using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Narucilac
{
    public class NarucilacDodajVM
    {
        
        public int NarucilacId { get; set; }

        [BindRequired]
        public int GradId { get; set; }
        public List<SelectListItem> gradLista { get; set; }

        [Required(ErrorMessage ="Ime i prezime je obavezno polje")]
        [RegularExpression("^.{4,}$", ErrorMessage = "Ime mora imat više od 4 slova")]
        public string ImePrezime { get; set; }

        [Required(ErrorMessage = "Korisnicko ime je obavezno polje")]
        [RegularExpression("^.{4,}$", ErrorMessage = "Ime mora imat više od 4 slova")]
        public string KorisnickoIme { get; set; }
        public string Email { get; set;}

        [Required(ErrorMessage = "Telefon je obavezno polje")]
        [RegularExpression("^[0-9]{6,11}$", ErrorMessage = "Telefon mora imat više od 5 cifri")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Adresa je obavezno polje")]
        [RegularExpression("^.{4,}$", ErrorMessage = "Adresa mora imat više od 4 slova")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        [RegularExpression("^.{8,}$", ErrorMessage = "Lozinka mora imat više od 8 karaktera")]
        public string Lozinka { get;  set; }
    }
}
