using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Examen.ApplicationCore.Domain.Infirmier;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IInfirmierService : IService<Infirmier>
    {
        double GetPourcentageInfirmiersParSpecialite(Specialite specialite);


    }
}
