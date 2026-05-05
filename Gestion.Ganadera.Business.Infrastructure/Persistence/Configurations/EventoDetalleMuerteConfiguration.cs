using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetalleMuerteConfiguration : IEntityTypeConfiguration<EventoDetalleMuerte>
{
    public void Configure(EntityTypeBuilder<EventoDetalleMuerte> entity)
    {
        entity.ToTable("Evento_Detalle_Muerte", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Causa_Muerte_Codigo)
            .IsRequired();

        entity.HasOne<CausaMuerte>()
            .WithMany()
            .HasForeignKey(x => x.Causa_Muerte_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Property(x => x.Evento_Detalle_Muerte_Fecha)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Muerte_Observacion)
            .HasMaxLength(500);

        entity.HasOne<EventoGanadero>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}