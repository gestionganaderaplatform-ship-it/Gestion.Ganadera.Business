using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetalleTrasladoFincaConfiguration : IEntityTypeConfiguration<EventoDetalleTrasladoFinca>
{
    public void Configure(EntityTypeBuilder<EventoDetalleTrasladoFinca> entity)
    {
        entity.ToTable("Evento_Detalle_Traslado_Finca", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Finca_Codigo_Origen)
            .IsRequired();

        entity.Property(x => x.Finca_Codigo_Destino)
            .IsRequired();

        entity.Property(x => x.Potrero_Codigo_Destino)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Traslado_Finca_Fecha)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Traslado_Finca_Observacion)
            .HasMaxLength(500);

        entity.HasOne<EventoGanadero>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
