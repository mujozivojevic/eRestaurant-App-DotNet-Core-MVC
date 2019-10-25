using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
   
    public class NaruciociPrikaziViewModel
    {
        public string NarucilacId { get; set; }
        public string ImePrezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
        public Grad grad { get; set; }
       
    }
}
