using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ImaSastojke
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("JeloId")]
        public Jelo Jelo { get; set; }
        public int JeloId { get; set; }

        [ForeignKey("SastojciId")]
        public Sastojci Sastojci { get; set; }
        public int SastojciId { get; set; }

    }
}
