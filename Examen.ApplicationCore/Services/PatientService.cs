using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Examen.ApplicationCore.Services
{
    public class PatientService : IPatientService
    {
        private readonly IAppDbContext _context;


        public PatientService(IAppDbContext context)
        {
            _context = context;
        }

        public IDictionary<Bilan, List<Analyse>> ObtenirAnalysesAnormalesParBilan(string codePatient)
        {
            int annee = DateTime.Now.Year;

            var bilans = _context.Bilans
                .Include(b => b.Analyse)
                .Where(b => b.CodePatient == codePatient && b.DatePrelevement.Year == annee)
                .ToList();

            return bilans
                .Where(b => b.Analyse.ValeurAnalyse < b.Analyse.ValeurMinNormale
                         || b.Analyse.ValeurAnalyse > b.Analyse.ValeurMaxNormale)
                .GroupBy(b => b)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(b => b.Analyse).ToList()
                );
        }
    }
}
