using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class AnalyseService : Service<Analyse>, IAnalyseService
    {
        public AnalyseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Analyse> GetAnalysesAnormalesDeLAnnee(Patient patient)
        {
            int currentYear = DateTime.Now.Year;

            var analyses = GetMany(a =>
                a.Bilans.Patient.CodePatient == patient.CodePatient &&
                a.Bilans.DatePrelevement.Year == currentYear &&
                (a.ValeurAnalyse < a.ValeurMinNormale || a.ValeurAnalyse > a.ValeurMaxNormale)
            );

            return analyses;
        }


    }
}
