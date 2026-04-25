using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestion.Ganadera.Business.Infrastructure.Security.Models;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public class EventoSeguridadConfiguration : IEntityTypeConfiguration<EventoSeguridad>
{
    public void Configure(EntityTypeBuilder<EventoSeguridad> entity)
    {
        entity.ToTable("Seguridad_Evento", "Seguridad");

        entity.HasKey(x => x.Evento_Seguridad_Codigo);

        entity.Property(x => x.Evento_Seguridad_Api_Codigo)
              .HasMaxLength(100)
              .IsRequired();

        entity.Property(x => x.Evento_Seguridad_Tipo_Evento)
              .HasMaxLength(100)
              .IsRequired();

        entity.Property(x => x.Evento_Seguridad_Ip)
              .HasMaxLength(45)
              .IsRequired();

        entity.Property(x => x.Evento_Seguridad_Endpoint)
              .HasMaxLength(500)
              .IsRequired();

        entity.Property(x => x.Evento_Seguridad_Origin)
              .HasMaxLength(200);

        entity.Property(x => x.Evento_Seguridad_UserAgent)
              .HasMaxLength(1000);

        entity.Property(x => x.Evento_Seguridad_CorrelationId)
              .HasMaxLength(100);

        entity.Property(x => x.Evento_Seguridad_Fecha)
              .HasDefaultValueSql("SYSDATETIME()");

        entity.Property(x => x.Cliente_Codigo);

        entity.HasIndex(x => x.Cliente_Codigo);
    }
}
