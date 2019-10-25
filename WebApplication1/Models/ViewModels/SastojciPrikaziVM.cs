using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public class SastojciPrikaziVM
    {
        public class Row
        { 
          public int Id { get; set; }
          public string Naziv { get; set; }
          public string Opis { get; set; }
        }
        public List<Row> htmlRows { get; set; }
    }
}
