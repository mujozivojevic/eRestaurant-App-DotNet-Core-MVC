﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class StatusDostave
    {
        [Key]
        public int Id { get; set;}
        public string Opis { get; set; }
    }
}
