using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.ViewModels
{
    public class RezervacijaPrikaziVM
    {
        public class StolVM
        {
            public int BrojStola { get; set; }
            public int BrojMjesta { get; set; }

        }
        public class Row
        {
            public int Id { get; set; }
            public DateTime DatumEvidencije { get; set; }
            public DateTime DatumRezervacije { get; set; }

           
            public Narucilac Narucilac { get; set; }

            public StolVM Stolovi { get; set; }
        }
        public List<Row> htmlRows { get; set; }
    }
}
