using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Analyse
    {
        public int AnalyseId { get; set; }
        public int DureeResultat { get; set; }
        public double PrixAnalyse { get; set; }
        public string TypeAnalyse { get; set; }

        public float ValeurAnalyse { get; set; }
        public float ValeurMaxNormale { get; set; }
        public float ValeurMinNormale { get; set; }

        public virtual Bilan Bilans { get; set; }
        public int BilanId { get; set; }
        public int LaboratoireId { get; set; }
    }
}
