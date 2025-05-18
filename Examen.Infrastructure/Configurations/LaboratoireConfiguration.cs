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
    public class LaboratoireConfiguration : IEntityTypeConfiguration<Laboratoire>
    {
        public void Configure(EntityTypeBuilder<Laboratoire> builder)
        {
            builder.Property(l => l.Localisation)
                .HasColumnName("AdresseLabo")
                .HasMaxLength(50);
        }
    }
}
