using System;
using Examen.Infrastructure.Data;
using Examen.ApplicationCore.Domain;

namespace Examen.UI.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---- Démarrage des tests EF Core ----");

            using (var context = new AppDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Console.WriteLine("\n✔️ Base de données recréée.");

                var labo = new Laboratoire
                {
                    Intitule = "Laboratoire Central",
                    Localisation = "Rue Principale"
                };
                context.Laboratoires.Add(labo);
                context.SaveChanges();
                Console.WriteLine($" Laboratoire ajouté : {labo.Intitule}");

                var infirmier = new Infirmier
                {
                    NomComplet = "Hadj Alouane",
                    Specialite = Specialite.Biochimie
                };
                labo.Infirmiers = new List<Infirmier> { infirmier };
                context.Infirmiers.Add(infirmier);
                context.SaveChanges();
                Console.WriteLine($"Infirmier ajouté : {infirmier.NomComplet} (spécialité : {infirmier.Specialite})");

                var patient = new Patient
                {
                    CodePatient = "P1001",
                    NomComplet = "Amina B.",
                    EmailPatient = "amina.b@example.com",
                    NumeroTel = "55112233",
                    Informations = "Allergique à la pénicilline"
                };
                context.Patients.Add(patient);
                context.SaveChanges();
                Console.WriteLine($" Patient ajouté : {patient.NomComplet}");

               
                var analyse = new Analyse
                {
                    TypeAnalyse = "Analyse de sang",
                    DureeResultat = 48,
                    PrixAnalyse = 120.0,
                    ValeurAnalyse = 4.8f,
                    ValeurMaxNormale = 6.0f,
                    ValeurMinNormale = 4.0f
                };
                context.Analyses.Add(analyse);
                context.SaveChanges();
                Console.WriteLine($" Analyse ajoutée : {analyse.TypeAnalyse} ({analyse.PrixAnalyse} DT)");

                var bilan = new Bilan
                {
                    DatePrelevement = DateTime.Now,
                    EmailMedecin = "dr.malek@hopital.tn",
                    Paye = true,
                    CodePatient = patient.CodePatient,
                    InfirmierId = infirmier.InfirmierId,
                    AnalyseId = analyse.AnalyseId
                };
                context.Bilans.Add(bilan);
                context.SaveChanges();
                Console.WriteLine(" Bilan créé et lié aux entités précédentes.");

                Console.WriteLine("\n Tous les tests sont terminés avec succès.");
            }

            Console.WriteLine("\nAppuyez sur une touche pour quitter...");
            Console.ReadKey();
        }
    }
}
