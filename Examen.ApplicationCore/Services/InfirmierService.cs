using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Examen.ApplicationCore.Services
{
    public class InfirmierService : IInfirmierService
    {
        private readonly IAppDbContext _context;

        public InfirmierService(IAppDbContext context)
        {
            _context = context;
        }

        public double ObtenirPourcentageParSpecialite(Specialite specialite)
        {
            var total = _context.Infirmiers.Count();
            if (total == 0)
                return 0;

            var nbSpec = _context.Infirmiers.Count(i => i.Specialite == specialite);
            return Math.Round((double)nbSpec * 100 / total, 2);
        }
    }
}
