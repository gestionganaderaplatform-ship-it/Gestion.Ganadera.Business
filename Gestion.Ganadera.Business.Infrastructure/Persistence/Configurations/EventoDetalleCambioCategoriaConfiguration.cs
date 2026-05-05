using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoDetalleCambioCategoriaConfiguration : IEntityTypeConfiguration<EventoDetalleCambioCategoria>
{
    public void Configure(EntityTypeBuilder<EventoDetalleCambioCategoria> entity)
    {
        entity.ToTable("Evento_Detalle_Cambio_Categoria", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Evento_Detalle_Cambio_Categoria_Peso_Al_Cambio)
            .HasPrecision(18, 2);

        entity.Property(x => x.Evento_Detalle_Cambio_Categoria_Observacion)
            .HasMaxLength(500);

        entity.HasOne(x => x.EventoGanadero)
            .WithOne()
            .HasForeignKey<EventoDetalleCambioCategoria>(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.CategoriaAnterior)
            .WithMany()
            .HasForeignKey(x => x.Categoria_Anterior_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.CategoriaNueva)
            .WithMany()
            .HasForeignKey(x => x.Categoria_Nueva_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
