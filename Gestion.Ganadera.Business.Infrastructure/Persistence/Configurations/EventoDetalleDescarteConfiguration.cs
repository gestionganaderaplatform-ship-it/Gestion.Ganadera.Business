using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetalleDescarteConfiguration : IEntityTypeConfiguration<EventoDetalleDescarte>
{
    public void Configure(EntityTypeBuilder<EventoDetalleDescarte> entity)
    {
        entity.ToTable("Evento_Detalle_Descarte", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Evento_Detalle_Descarte_Fecha)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Descarte_Destino)
            .HasMaxLength(200);

        entity.Property(x => x.Evento_Detalle_Descarte_Valor)
            .HasPrecision(18, 2);

        entity.Property(x => x.Evento_Detalle_Descarte_Observacion)
            .HasMaxLength(1000);

        entity.HasOne(x => x.Evento_Ganadero)
            .WithOne()
            .HasForeignKey<EventoDetalleDescarte>(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Descarte_Motivo)
            .WithMany()
            .HasForeignKey(x => x.Descarte_Motivo_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
