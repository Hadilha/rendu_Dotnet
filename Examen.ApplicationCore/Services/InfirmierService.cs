using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Examen.ApplicationCore.Domain.Infirmier;


namespace Examen.ApplicationCore.Services
{
    public class InfirmierService : Service<Infirmier>, IInfirmierService
    {
        public InfirmierService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public double GetPourcentageInfirmiersParSpecialite(Specialite specialite)
        {
            var allInfirmiers = GetMany();

            int total = allInfirmiers.Count();

            if (total == 0)
                return 0;

            int matching = allInfirmiers.Count(i => i.specialite == specialite);

            return (matching / (double)total) * 100;
        
    }
    }
}
