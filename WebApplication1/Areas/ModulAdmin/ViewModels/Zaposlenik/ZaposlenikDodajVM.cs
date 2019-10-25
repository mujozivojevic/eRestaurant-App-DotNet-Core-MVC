using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Zaposlenik
{
    public class ZaposlenikDodajVM
    {
   
        public int Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno polje")]
        [RegularExpression("^.{3,}$", ErrorMessage = "Ime mora imat više od 3 slova")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Ime je obavezno polje")]
        [RegularExpression("^.{3,}$", ErrorMessage = "Prezime mora imat više od 3 slova")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "JMBG je obavezno polje")]
        [RegularExpression("^(\\d{13})?$", ErrorMessage = "JMBG mora imati 13 cifri")]
        public string JMBG { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon je obavezno polje")]
        [RegularExpression("^[0-9]{6,11}$", ErrorMessage = "Telefon mora imat više od 5 cifri")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Adresa je obavezno polje")]
        [RegularExpression("^.{4,}$", ErrorMessage = "Adresa mora imat više od 4 slova")]
        public string Adresa { get; set; }



        [Required(ErrorMessage = "Korisničko ime je obavezno polje")]
        [RegularExpression("^.{3,}$", ErrorMessage = "Korisničko ime mora imat više od 3 slova")]
        public string KorisnickoIme { get; set; }

        
        [Required(ErrorMessage = "Lozinka je obavezno polje")]
        [RegularExpression("^.{8,}$", ErrorMessage = "Lozinka mora imat više od 8 karaktera")]
        public string Lozinka { get; set; }

        [BindRequired]
        public int GradID { get; set; }
        public List<SelectListItem> GradLista { get; set; }

        [BindRequired]
        public int RadnoMjestoId { get; set; }
        public List<SelectListItem> RadnoMjestoList { get; set; }

 



    }
}
