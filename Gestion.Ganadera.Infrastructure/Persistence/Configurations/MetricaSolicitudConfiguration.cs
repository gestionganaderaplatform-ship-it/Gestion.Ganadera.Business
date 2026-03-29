using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestion.Ganadera.Infrastructure.Observability.Models;

namespace Gestion.Ganadera.Infrastructure.Persistence.Configurations;

public class MetricaSolicitudConfiguration : IEntityTypeConfiguration<MetricaSolicitud>
{
    public void Configure(EntityTypeBuilder<MetricaSolicitud> entity)
    {
        entity.ToTable("Metrica_Solicitud", "Seguridad");

        entity.HasKey(x => x.Metrica_Solicitud_Codigo);

        entity.Property(x => x.Metrica_Solicitud_Api_Codigo)
              .HasMaxLength(100)
              .IsRequired();

        entity.Property(x => x.Metrica_Solicitud_Metodo_Http)
              .HasMaxLength(10)
              .IsRequired();

        entity.Property(x => x.Metrica_Solicitud_Ruta_Request)
              .HasMaxLength(500)
              .IsRequired();

        entity.Property(x => x.Metrica_Solicitud_Correlation_Id)
              .HasMaxLength(100);

        entity.Property(x => x.Metrica_Solicitud_Fecha_Creacion)
              .HasDefaultValueSql("SYSDATETIME()");
    }
}
