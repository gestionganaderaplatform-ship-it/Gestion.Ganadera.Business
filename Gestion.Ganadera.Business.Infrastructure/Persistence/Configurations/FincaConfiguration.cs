using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class FincaConfiguration : IEntityTypeConfiguration<Finca>
{
    public void Configure(EntityTypeBuilder<Finca> entity)
    {
        entity.ToTable("Finca", "Ganaderia");

        entity.HasKey(x => x.Finca_Codigo);

        entity.HasIndex(x => new { x.Cliente_Codigo, x.Finca_Nombre })
            .IsUnique();

        entity.Property(x => x.Finca_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Finca_Nombre)
            .HasMaxLength(200)
            .IsRequired();

        entity.ConfigureAuditableGanaderia();
    }
}
