using Examen.ApplicationCore.Domain;
using Examen.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Infrastructure
{
    public class EXContext : DbContext
    {
        DbSet<Patient> Patients { get; }
        DbSet<Infirmier> Infirmiers { get; }
        DbSet<Laboratoire> Laboratoires { get; }
        DbSet<Analyse> Analyses { get; }
        DbSet<Bilan> Bilans { get; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
            Initial Catalog=HadilHadAlouane;Integrated Security=true;
                MultipleActiveResultSets=true");

            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
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
