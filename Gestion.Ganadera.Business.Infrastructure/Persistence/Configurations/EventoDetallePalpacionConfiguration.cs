using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetallePalpacionConfiguration : IEntityTypeConfiguration<EventoDetallePalpacion>
{
    public void Configure(EntityTypeBuilder<EventoDetallePalpacion> entity)
    {
        entity.ToTable("Evento_Detalle_Palpacion", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Evento_Detalle_Palpacion_Fecha)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Palpacion_Responsable)
            .HasMaxLength(150);

        entity.Property(x => x.Evento_Detalle_Palpacion_Dato_Complementario)
            .HasMaxLength(500);

        entity.Property(x => x.Evento_Detalle_Palpacion_Observacion)
            .HasMaxLength(1000);

        entity.HasOne(x => x.Evento_Ganadero)
            .WithOne()
            .HasForeignKey<EventoDetallePalpacion>(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Palpacion_Resultado)
            .WithMany()
            .HasForeignKey(x => x.Palpacion_Resultado_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
