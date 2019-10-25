using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.EF
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<Narucilac> Narucilac { get; set; }

        public DbSet<Dostava> Dostava { get; set; }
        public DbSet<Jelo> Jelo { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<NarudzbaStavke> NarudzbaStavke { get; set; }
        public DbSet<StatusDostave> StatusDostave { get; set; }
        public DbSet<Sastojci> Sastojci { get; set; }

        public DbSet<ImaSastojke> ImaSastojke { get; set; }
        public DbSet<VrstaJela> VrstaJela { get; set; }

        public DbSet<Obavijest> Obavijest { get; set; }
        public DbSet<Zaposlenik> Zaposlenik { get; set; }
        public DbSet<Dolasci> Dolasci { get; set; }
        public DbSet<DatumMjesec> DatumMjesec { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<Stol> Stol { get; set; }
        public DbSet<Plata> Plata { get; set; }
        public DbSet<RadnoMjesto> RadnoMjesto { get; set; }
        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }


        public DbSet<RezervacijaStavke> RezervacijaStavke { get; set; }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<NarudzbaStavke>().HasKey(ns => new { ns.JeloId, ns.NarudzbaId });
           

        }

    }
}
