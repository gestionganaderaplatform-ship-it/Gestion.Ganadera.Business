using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestion.Ganadera.Business.Infrastructure.Observability.Models;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public class LogAplicacionConfiguration : IEntityTypeConfiguration<LogAplicacion>
{
    public void Configure(EntityTypeBuilder<LogAplicacion> entity)
    {
        entity.ToTable("Log_Aplicacion", "Seguridad");

        entity.HasKey(x => x.Log_Aplicacion_Codigo);

        entity.Property(x => x.Log_Aplicacion_Codigo)
              .ValueGeneratedOnAdd();

        entity.Property(x => x.Log_Aplicacion_Mensaje)
              .HasColumnName("Log_Aplicacion_Mensaje");

        entity.Property(x => x.Log_Aplicacion_Nivel)
              .HasColumnName("Log_Aplicacion_Nivel")
              .HasMaxLength(128);

        entity.Property(x => x.Log_Aplicacion_Fecha)
              .HasColumnName("Log_Aplicacion_Fecha");

        entity.Property(x => x.Log_Aplicacion_Excepcion)
              .HasColumnName("Log_Aplicacion_Excepcion");

        entity.Property(x => x.Log_Aplicacion_Api_Codigo)
              .HasColumnName("Log_Aplicacion_Api_Codigo")
              .HasMaxLength(100);

        entity.Property(x => x.Log_Aplicacion_Origen)
              .HasColumnName("Log_Aplicacion_Origen")
              .HasMaxLength(128);

        entity.Property(x => x.Log_Aplicacion_Metodo)
              .HasColumnName("Log_Aplicacion_Metodo")
              .HasMaxLength(128);

        entity.Property(x => x.Log_Aplicacion_Ruta)
              .HasColumnName("Log_Aplicacion_Ruta")
              .HasMaxLength(500);

        entity.Property(x => x.Log_Aplicacion_Usuario)
              .HasColumnName("Log_Aplicacion_Usuario")
              .HasMaxLength(200);

        entity.Property(x => x.Log_Aplicacion_CorrelationId)
              .HasColumnName("Log_Aplicacion_CorrelationId")
              .HasMaxLength(100);

        entity.HasIndex(x => x.Log_Aplicacion_Fecha);
        entity.HasIndex(x => new { x.Log_Aplicacion_Nivel, x.Log_Aplicacion_Fecha });
        entity.HasIndex(x => x.Cliente_Codigo);
    }
}
