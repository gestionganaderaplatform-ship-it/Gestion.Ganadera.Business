using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class CausaMuerteConfiguration : IEntityTypeConfiguration<CausaMuerte>
{
    public void Configure(EntityTypeBuilder<CausaMuerte> entity)
    {
        entity.ToTable("Causa_Muerte", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Causa_Muerte_Codigo);

        entity.Property(x => x.Causa_Muerte_Nombre)
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.Causa_Muerte_Descripcion)
            .HasMaxLength(500);

        entity.Property(x => x.Causa_Muerte_Activa)
            .HasDefaultValue(true);
            
        entity.Property(x => x.Causa_Muerte_Orden)
            .HasDefaultValue(0);
    }
}
