using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class DescarteMotivoConfiguration : IEntityTypeConfiguration<DescarteMotivo>
{
    public void Configure(EntityTypeBuilder<DescarteMotivo> entity)
    {
        entity.ToTable("Descarte_Motivo", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Descarte_Motivo_Codigo);

        entity.Property(x => x.Descarte_Motivo_Nombre)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(x => x.Descarte_Motivo_Activo)
            .HasDefaultValue(true);
    }
}
