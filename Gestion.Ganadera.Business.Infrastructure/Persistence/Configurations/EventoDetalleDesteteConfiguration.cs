using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetalleDesteteConfiguration : IEntityTypeConfiguration<EventoDetalleDestete>
{
    public void Configure(EntityTypeBuilder<EventoDetalleDestete> entity)
    {
        entity.ToTable("Evento_Detalle_Destete", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Evento_Detalle_Destete_Fecha)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Destete_Responsable)
            .HasMaxLength(150);

        entity.Property(x => x.Evento_Detalle_Destete_Observacion)
            .HasMaxLength(1000);

        entity.HasOne(x => x.Evento_Ganadero)
            .WithOne()
            .HasForeignKey<EventoDetalleDestete>(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<Animal>()
            .WithMany()
            .HasForeignKey(x => x.Animal_Codigo_Madre)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<Potrero>()
            .WithMany()
            .HasForeignKey(x => x.Potrero_Destino_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
