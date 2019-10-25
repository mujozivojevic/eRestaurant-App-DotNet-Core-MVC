using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels.Login
{
    public class LoginVM
    {

        [StringLength(100, ErrorMessage = "Korsničko ime mora sadržavati minimalno 4 karaktera", MinimumLength = 4)]
        public string KorisnickoIme { get; set; }

        [StringLength(100, ErrorMessage = "Password mora sadržavati minimalno 8 karaktera", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        public bool ZapamtiPassword { get; set; }
    }
}
