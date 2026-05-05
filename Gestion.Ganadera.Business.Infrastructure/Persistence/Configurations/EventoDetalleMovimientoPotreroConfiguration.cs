using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetalleMovimientoPotreroConfiguration : IEntityTypeConfiguration<EventoDetalleMovimientoPotrero>
{
    public void Configure(EntityTypeBuilder<EventoDetalleMovimientoPotrero> entity)
    {
        entity.ToTable("Evento_Detalle_Movimiento_Potrero", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Potrero_Codigo_Destino)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Movimiento_Potrero_Fecha)
            .IsRequired();

        entity.HasOne<EventoGanadero>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}