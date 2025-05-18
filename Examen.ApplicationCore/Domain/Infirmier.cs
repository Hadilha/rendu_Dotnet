using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Infirmier
    {
        public int InfirmierId { get; set; }
        public string NomComplet { get; set; } = string.Empty;
        public Specialite Specialite { get; set; }

        public ICollection<Bilan> Bilans { get; set; } = new List<Bilan>();
    }

}
