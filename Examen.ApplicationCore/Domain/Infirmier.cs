using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Infirmier
    {
        public enum Specialite
        {
            Hematologie,
            Biochimie,
            Autre
        }
        public int InfirmierId { get; set; }
        public string NomComplet { get; set; }
        public Specialite  specialite { get; set; }

        public virtual ICollection<Bilan> Bilans { get; set; }
        public virtual Laboratoire Laboratoire { get; set; }
        public int LaboratoireFK { get; set; }

    }

}
