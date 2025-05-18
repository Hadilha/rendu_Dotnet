using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Examen.ApplicationCore.Services;
using Examen.Infrastructure.Data;

namespace Examen.UI.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Démarrage du test du Labo  !\n");

            //  1) On prépare notre conteneur de services et EF Core
            var services = new ServiceCollection();
            services.AddDbContext<IAppDbContext, AppDbContext>(opts =>
                opts.UseSqlServer("Server=.;Database=LaboHadil;Trusted_Connection=True;"));
            services.AddScoped<IBilanService, BilanService>();
            services.AddScoped<IInfirmierService, InfirmierService>();
            services.AddScoped<IPatientService, PatientService>();

            var provider = services.BuildServiceProvider();

            //  2) On crée une portée pour peupler la base
            using (var scope = provider.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

                Console.WriteLine("🔄 Réinitialisation de la base de données…");
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
                Console.WriteLine("✅ Base remise à zéro.\n");

                // --- Création d’un laboratoire ---
                var labo = new Laboratoire
                {
                    Intitule = "Laboratoire El-Medina",
                    Localisation = "Avenue Habib Bourguiba"
                };
                ctx.Laboratoires.Add(labo);

                // --- Deux infirmiers (Mohamed & Sarra) ---
                var infirmierMohamed = new Infirmier
                {
                    NomComplet = "Mohamed Ben Ali",
                    Specialite = Specialite.Hematologie
                };
                var infirmierSarra = new Infirmier
                {
                    NomComplet = "Sarra Lamine",
                    Specialite = Specialite.Biochimie
                };
                labo.Infirmiers = new List<Infirmier> { infirmierMohamed, infirmierSarra };
                ctx.Infirmiers.AddRange(infirmierMohamed, infirmierSarra);

                // --- Un patient de test (Yasmine) ---
                var patientYasmine = new Patient
                {
                    CodePatient = "H2024",
                    NomComplet = "Yasmine Trabelsi",
                    EmailPatient = "yasmine.trabelsi@example.tn",
                    NumeroTel = "98 765 432",
                    Informations = "Pas d’allergies connues"
                };
                ctx.Patients.Add(patientYasmine);

                // --- Une analyse (Globule Rouge) ---
                var analyse = new Analyse
                {
                    TypeAnalyse = "Numération Globulaire",
                    DureeResultat = 48,
                    PrixAnalyse = 65.0,
                    ValeurMinNormale = 4.0f,
                    ValeurMaxNormale = 5.5f,
                    ValeurAnalyse = 4.7f,
                    // Note : on fixera LaboratoireId après SaveChanges
                };
                ctx.Analyses.Add(analyse);

                ctx.SaveChanges();

                // On attribue l’analyse et l’infirmier Mohamed au labo
                analyse = ctx.Analyses.First();
                infirmierMohamed = ctx.Infirmiers.First(i => i.NomComplet.StartsWith("Mohamed"));

                // --- Création de bilans ---
                // Bilan 1 : 1er prélèvement il y a 7 jours (pas de remise)
                var bilan1 = new Bilan
                {
                    DatePrelevement = DateTime.Now.AddDays(-7),
                    EmailMedecin = "dr.khaled@hopital.tn",
                    Paye = true,
                    CodePatient = patientYasmine.CodePatient,
                    InfirmierId = infirmierMohamed.InfirmierId,
                    AnalyseId = analyse.AnalyseId
                };

                // Bilan 2 : 8ème prélèvement aujourd’hui (remise automatique)
                // On simule 7 prélèvements précédents pour dépasser le seuil
                for (int i = 1; i <= 7; i++)
                {
                    ctx.Bilans.Add(new Bilan
                    {
                        DatePrelevement = DateTime.Now.AddDays(-30 - i),
                        EmailMedecin = "dr.khaled@hopital.tn",
                        Paye = true,
                        CodePatient = patientYasmine.CodePatient,
                        InfirmierId = infirmierMohamed.InfirmierId,
                        AnalyseId = analyse.AnalyseId
                    });
                }

                var bilan2 = new Bilan
                {
                    DatePrelevement = DateTime.Now,
                    EmailMedecin = "dr.khaled@hopital.tn",
                    Paye = false,
                    CodePatient = patientYasmine.CodePatient,
                    InfirmierId = infirmierMohamed.InfirmierId,
                    AnalyseId = analyse.AnalyseId
                };
                ctx.Bilans.AddRange(bilan1, bilan2);

                ctx.SaveChanges();
                Console.WriteLine(" Données de test insérées.\n");

                //  3) On récupère nos services pour les tester
                var bilanSvc = scope.ServiceProvider.GetRequiredService<IBilanService>();
                var infirmierSvc = scope.ServiceProvider.GetRequiredService<IInfirmierService>();
                var patientSvc = scope.ServiceProvider.GetRequiredService<IPatientService>();

                // --- Test du calcul du montant ---
                Console.WriteLine(" Montants à payer :");
                Console.WriteLine($" • 1er prélèvement : {bilanSvc.CalculerMontantTotal(bilan1):0.00} DT");
                Console.WriteLine($" • 8ème prélèvement (remise) : {bilanSvc.CalculerMontantTotal(bilan2):0.00} DT\n");

                // --- Test de la date de récupération ---
                var recup = bilanSvc.ObtenirDateRecuperation(bilan2);
                Console.WriteLine($" Résultat disponible le : {recup:dd/MM/yyyy HH:mm}\n");

                // --- Statistiques infirmiers ---
                Console.WriteLine(" Répartition des spécialités :");
                var pctHema = infirmierSvc.ObtenirPourcentageParSpecialite(Specialite.Hematologie);
                var pctBio = infirmierSvc.ObtenirPourcentageParSpecialite(Specialite.Biochimie);
                Console.WriteLine($" • Hématologie : {pctHema}%");
                Console.WriteLine($" • Biochimie : {pctBio}%\n");

                // --- Analyses anormales de Yasmine ---
                Console.WriteLine(" Analyses hors normes (Yasmine) :");
                var anomalies = patientSvc.ObtenirAnalysesAnormalesParBilan(patientYasmine.CodePatient);
                if (!anomalies.Any())
                {
                    Console.WriteLine(" Aucune anomalie détectée cette année.");
                }
                else
                {
                    foreach (var kv in anomalies)
                    {
                        Console.WriteLine($"  Bilan du {kv.Key.DatePrelevement:dd/MM/yyyy} :");
                        foreach (var a in kv.Value)
                            Console.WriteLine($"    - {a.TypeAnalyse} = {a.ValeurAnalyse}");
                    }
                }
            }

            Console.WriteLine("\nMerci d’avoir testé ! Appuyez sur une touche pour quitter.");
            Console.ReadKey();
        }
    }
}
