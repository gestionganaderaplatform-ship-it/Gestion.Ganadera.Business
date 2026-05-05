using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetalleVacunacionConfiguration : IEntityTypeConfiguration<EventoDetalleVacunacion>
{
    public void Configure(EntityTypeBuilder<EventoDetalleVacunacion> entity)
    {
        entity.ToTable("Evento_Detalle_Vacunacion", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Evento_Detalle_Vacunacion_Fecha)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Vacunacion_Vacuna_Codigo)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Vacunacion_Ciclo)
            .HasMaxLength(30)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Vacunacion_Lote)
            .HasMaxLength(50);

        entity.Property(x => x.Evento_Detalle_Vacunacion_Vacunador)
            .HasMaxLength(120);

        entity.Property(x => x.Evento_Detalle_Vacunacion_Dosis)
            .HasMaxLength(30);

        entity.Property(x => x.Evento_Detalle_Vacunacion_Observacion)
            .HasMaxLength(500);

        entity.HasOne<EventoGanadero>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<Vacuna>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Detalle_Vacunacion_Vacuna_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<VacunaEnfermedad>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Detalle_Vacunacion_Enfermedad_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}