using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class BilanService : Service<Bilan>, IBilanService
    {
        public BilanService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public double CalculerMontantTotal(Bilan bilan)
        {
            var total = bilan.Analyses.Sum(a => a.PrixAnalyse);

            int nombrePrelevements = GetMany(b => b.PatientFk == bilan.PatientFk).Count();

            if (nombrePrelevements > 5)
            {
                total *= 0.9; 
            }

            return total;
        }


        public DateTime? GetDateRecuperationBilan(Bilan b)
        {
            if (b == null || b.Analyses == null || !b.Analyses.Any())
                return null;

            var dateMax = b.Analyses.Max(a => b.DatePrelevement.AddDays(a.DureeResultat));
            return dateMax;
        }


    }
}
