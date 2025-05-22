using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Examen.ApplicationCore.Services
{
    public class PatientService : Service<Patient>, IPatientService
    {
        public PatientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

      
    }
}
