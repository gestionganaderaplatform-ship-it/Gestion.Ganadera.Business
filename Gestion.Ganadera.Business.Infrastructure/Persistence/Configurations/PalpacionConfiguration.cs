using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class PalpacionResultadoConfiguration : IEntityTypeConfiguration<PalpacionResultado>
{
    public void Configure(EntityTypeBuilder<PalpacionResultado> entity)
    {
        entity.ToTable("Palpacion_Resultado", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Palpacion_Resultado_Codigo);

        entity.Property(x => x.Palpacion_Resultado_Nombre)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(x => x.Palpacion_Resultado_Activo)
            .HasDefaultValue(true);
    }
}
