using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Patient> Patients { get; }
        DbSet<Infirmier> Infirmiers { get; }
        DbSet<Laboratoire> Laboratoires { get; }
        DbSet<Analyse> Analyses { get; }
        DbSet<Bilan> Bilans { get; }

        int SaveChanges();
        DatabaseFacade Database { get; }
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
