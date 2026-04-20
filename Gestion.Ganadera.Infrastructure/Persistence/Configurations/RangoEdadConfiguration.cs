using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Infrastructure.Persistence.Configurations;

public sealed class RangoEdadConfiguration : IEntityTypeConfiguration<RangoEdad>
{
    public void Configure(EntityTypeBuilder<RangoEdad> entity)
    {
        entity.ToTable("Rango_Edad", "Ganaderia");

        entity.HasKey(x => x.Rango_Edad_Codigo);

        entity.HasIndex(x => new { x.Cliente_Codigo, x.Rango_Edad_Nombre })
            .IsUnique();

        entity.Property(x => x.Rango_Edad_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Rango_Edad_Nombre)
            .HasMaxLength(120)
            .IsRequired();

        entity.ConfigureAuditableGanaderia();
    }
}
