using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetalleTratamientoSanitarioConfiguration : IEntityTypeConfiguration<EventoDetalleTratamientoSanitario>
{
    public void Configure(EntityTypeBuilder<EventoDetalleTratamientoSanitario> entity)
    {
        entity.ToTable("Evento_Detalle_Tratamiento_Sanitario", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Evento_Detalle_Tratamiento_Fecha)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Tratamiento_Dosis)
            .HasPrecision(18, 2);

        entity.Property(x => x.Evento_Detalle_Tratamiento_Duracion)
            .HasMaxLength(100);

        entity.Property(x => x.Evento_Detalle_Tratamiento_Indicacion)
            .HasMaxLength(500);

        entity.Property(x => x.Evento_Detalle_Tratamiento_Aplicador)
            .HasMaxLength(150);

        entity.Property(x => x.Evento_Detalle_Tratamiento_Observacion)
            .HasMaxLength(1000);

        entity.HasOne(x => x.Evento_Ganadero)
            .WithOne()
            .HasForeignKey<EventoDetalleTratamientoSanitario>(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Tratamiento_Producto)
            .WithMany()
            .HasForeignKey(x => x.Tratamiento_Producto_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
