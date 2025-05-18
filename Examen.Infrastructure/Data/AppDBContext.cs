using System;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Examen.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Laboratoire> Laboratoires { get; set; }
        public DbSet<Infirmier> Infirmiers { get; set; }
        public DbSet<Analyse> Analyses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Bilan> Bilans { get; set; }
        public int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bilan>()
                .HasKey(b => new { b.InfirmierId, b.CodePatient, b.DatePrelevement });

            modelBuilder.Entity<Bilan>()
                .HasOne(b => b.Patient)
                .WithMany(p => p.Bilans)
                .HasForeignKey(b => b.CodePatient)
                .HasPrincipalKey(p => p.CodePatient);

            modelBuilder.Entity<Bilan>()
                .HasOne(b => b.Infirmier)
                .WithMany(i => i.Bilans)
                .HasForeignKey(b => b.InfirmierId);

            modelBuilder.Entity<Bilan>()
                .HasOne(b => b.Analyse)
                .WithMany(a => a.Bilans)
                .HasForeignKey(b => b.AnalyseId);

            // Seed uniquement pour Laboratoire, Patient, Analyse, Bilan (PAS pour Infirmier)
            modelBuilder.Entity<Laboratoire>().HasData(new Laboratoire
            {
                LaboratoireId = 1,
                Intitule = "Laboratoire Central",
                Localisation = "Centre-ville"
            });

            modelBuilder.Entity<Patient>().HasData(new Patient
            {
                CodePatient = "P0001",
                NomComplet = "Sarra Trabelsi",
                EmailPatient = "sarra@example.tn",
                NumeroTel = "99887766",
                Informations = "Aucune"
            });

            modelBuilder.Entity<Analyse>().HasData(new Analyse
            {
                AnalyseId = 1,
                TypeAnalyse = "Numération globulaire",
                DureeResultat = 24,
                PrixAnalyse = 50.0,
                ValeurMinNormale = 4.0f,
                ValeurMaxNormale = 5.5f,
                ValeurAnalyse = 4.8f,
                LaboratoireId = 1
            });

           
        }
    }
}
