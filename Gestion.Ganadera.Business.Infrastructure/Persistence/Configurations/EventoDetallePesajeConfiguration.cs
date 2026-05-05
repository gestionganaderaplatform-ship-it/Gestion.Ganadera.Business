using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetallePesajeConfiguration : IEntityTypeConfiguration<EventoDetallePesaje>
{
    public void Configure(EntityTypeBuilder<EventoDetallePesaje> entity)
    {
        entity.ToTable("Evento_Detalle_Pesaje", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Evento_Detalle_Peso)
            .HasPrecision(18, 2)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Pesaje_Observacion)
            .HasMaxLength(500);

        entity.HasOne<EventoGanadero>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}