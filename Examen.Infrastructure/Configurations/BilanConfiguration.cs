using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Examen.Infrastructure.Configurations
{
    public class BilanConfiguration : IEntityTypeConfiguration<Bilan>
    {
        public void Configure(EntityTypeBuilder<Bilan> builder)
        {
            builder.HasKey(b => new { b.InfirmierFk, b.PatientFk, b.DatePrelevement });

            builder.HasOne(b => b.Infirmier)
                   .WithMany(i => i.Bilans)
                   .HasForeignKey(b => b.InfirmierFk);

            builder.HasOne(b => b.Patient)
                   .WithMany(p => p.Bilans)
                   .HasForeignKey(b => b.PatientFk);
        }

    }
}
