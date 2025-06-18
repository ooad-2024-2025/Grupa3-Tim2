using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Slasticarna.Models;
using Microsoft.Identity.Client;

namespace Slasticarna.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Popust> Popusti { get; set; }
        public DbSet<VrstaProizvoda> VrsteProizvoda { get; set; }
        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Narudzba> Narudzbe { get; set; }
        public DbSet<StavkaNarudzbe> StavkeNarudzbe { get; set; }
        public DbSet<NacinPlacanja> NaciniPlacanja { get; set; }
        public DbSet<Placanje> Placanja { get; set; }
        public DbSet<Ocjena> Ocjene { get; set; }
        public DbSet<StanjeNarudzbe> StanjaNarudzbi { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<PraznicnaPonuda> PraznicnePonude { get; set; }
        public DbSet<PraznicnaPonudaProizvod> PraznicnaPonudaProizvodi { get; set; }
        public DbSet<PopustProizvod> PopustProizvodi { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Popust>().ToTable("Popust");
            modelBuilder.Entity<VrstaProizvoda>().ToTable("VrstaProizvoda");
            modelBuilder.Entity<Proizvod>().ToTable("Proizvod");
            modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
            modelBuilder.Entity<StavkaNarudzbe>().ToTable("StavkeNarudzbe");
            modelBuilder.Entity<NacinPlacanja>().ToTable("NacinPlacanja");
            modelBuilder.Entity<Placanje>().ToTable("Placanje");
            modelBuilder.Entity<Ocjena>().ToTable("Ocjena");
            modelBuilder.Entity<StanjeNarudzbe>().ToTable("StanjeNarudzbe");
            modelBuilder.Entity<ApplicationUser>().ToTable("AppUsers");
            modelBuilder.Entity<PraznicnaPonuda>().ToTable("PraznicnaPonuda");
            modelBuilder.Entity<PraznicnaPonudaProizvod>().ToTable("PraznicnaPonudaProizvodi");
            modelBuilder.Entity<PopustProizvod>().ToTable("PopustProizvodi");

            // Popust - Proizvod relationship
            modelBuilder.Entity<PopustProizvod>()
                .HasKey(pp => new { pp.PopustID, pp.ProizvodID });

            modelBuilder.Entity<PopustProizvod>()
                .HasOne(pp => pp.Popust)
                .WithMany(p => p.PopustProizvodi)
                .HasForeignKey(pp => pp.PopustID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<PopustProizvod>()
                .HasOne(pp => pp.Proizvod)
                .WithMany()
                .HasForeignKey(pp => pp.ProizvodID)
                .OnDelete(DeleteBehavior.Cascade);

            // Cascade path rješavanje
            modelBuilder.Entity<Ocjena>()
                .HasOne(o => o.Narudzba)
                .WithMany()
                .HasForeignKey(o => o.NarudzbaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ocjena>()
                .HasOne(o => o.Korisnik)
                .WithMany()
                .HasForeignKey(o => o.KorisnikID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Placanje>()
                .HasOne(p => p.Narudzba)
                .WithMany()
                .HasForeignKey(p => p.NarudzbaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Placanje>()
                .HasOne(p => p.Popust)
                .WithMany()
                .HasForeignKey(p => p.PopustID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PraznicnaPonudaProizvod>()
        .HasKey(pp => new { pp.PraznicnaPonudaID, pp.ProizvodID });

            modelBuilder.Entity<PraznicnaPonudaProizvod>()
                .HasOne(pp => pp.PraznicnaPonuda)
                .WithMany(p => p.Proizvodi)
                .HasForeignKey(pp => pp.PraznicnaPonudaID);

            modelBuilder.Entity<PraznicnaPonudaProizvod>()
                .HasOne(pp => pp.Proizvod)
                .WithMany()
                .HasForeignKey(pp => pp.ProizvodID);

        }
    }
}
