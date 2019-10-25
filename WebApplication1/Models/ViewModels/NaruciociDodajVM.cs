using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public class NaruciociDodajVM
    {
        
        public List<SelectListItem> gradLista { get; set; }
        public Narucilac narucilac { get; set; }



    }
}
