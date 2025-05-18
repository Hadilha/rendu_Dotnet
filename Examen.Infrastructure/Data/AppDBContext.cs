using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examen.ApplicationCore.Domain;
using Examen.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Examen.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Laboratoire> Laboratoires { get; set; }
        public DbSet<Infirmier> Infirmiers { get; set; }
        public DbSet<Analyse> Analyses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Bilan> Bilans { get; set; }

        public AppDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LaboHadilHadjAlouane;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BilanConfiguration());

            modelBuilder.Entity<Laboratoire>()
                .Property(l => l.Localisation)
                .HasColumnName("AdresseLabo")
                .HasMaxLength(50);
        }
    }
}
