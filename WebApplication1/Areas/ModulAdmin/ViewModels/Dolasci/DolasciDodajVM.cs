using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Areas.ModulAdmin.ViewModels.Dolasci
{
    public class DolasciDodajVM
    {
        public int Id { get; set; }

        [BindRequired]
        public int ZaposlenikId { get; set; }
        public List<SelectListItem> ZaposleniciList { get; set; }
    }
}
