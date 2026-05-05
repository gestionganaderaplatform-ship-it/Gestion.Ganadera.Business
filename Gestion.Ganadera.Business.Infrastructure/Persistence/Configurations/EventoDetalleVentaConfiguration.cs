using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetalleVentaConfiguration : IEntityTypeConfiguration<EventoDetalleVenta>
{
    public void Configure(EntityTypeBuilder<EventoDetalleVenta> entity)
    {
        entity.ToTable("Evento_Detalle_Venta", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Evento_Detalle_Venta_Comprador)
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Venta_Fecha)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Venta_Valor)
            .HasPrecision(18, 2);

        entity.Property(x => x.Evento_Detalle_Venta_Observacion)
            .HasMaxLength(500);

        entity.HasOne<EventoGanadero>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}