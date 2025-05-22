using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Bilan
    {
        public DateTime DatePrelevement { get; set; }
        public string EmailMedecin { get; set; }
        public bool Paye { get; set; }

        public int InfirmierFk { get; set; }
        public virtual Infirmier Infirmier { get; set; }

        public virtual  Patient Patient { get; set; }
        public string PatientFk { get; set; }


        public virtual ICollection<Analyse> Analyses { get; set; }

    }
}
