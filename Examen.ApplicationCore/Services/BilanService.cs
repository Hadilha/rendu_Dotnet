using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class BilanService : IBilanService
    {
        private readonly IAppDbContext _context;



        public BilanService(IAppDbContext context)
        {
            _context = context;
        }

        public decimal CalculerMontantTotal(Bilan bilan)
        {
            // Nombre de bilans précédents pour ce patient
            var countPrecedents = _context.Bilans
                .Count(b => b.CodePatient == bilan.CodePatient
                         && b.DatePrelevement < bilan.DatePrelevement);

            // Charger Analyse si nécessaire
            if (bilan.Analyse == null)
            {
                _context.Entry(bilan)
                        .Reference(b => b.Analyse)
                        .Load();
            }

            decimal prix = (decimal)bilan.Analyse.PrixAnalyse;

            // Appliquer 10% de remise si plus de 5 prélèvements
            if (countPrecedents > 5)
                prix *= 0.9m;

            return prix;
        }

        public DateTime ObtenirDateRecuperation(Bilan bilan)
        {
            if (bilan.Analyse == null)
            {
                _context.Entry(bilan)
                        .Reference(b => b.Analyse)
                        .Load();
            }
            return bilan.DatePrelevement.AddHours(bilan.Analyse.DureeResultat);
        }
    }
}
