using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestion.Ganadera.Infrastructure.Security.Models;

namespace Gestion.Ganadera.Infrastructure.Persistence.Configurations;

public class EventoSeguridadConfiguration : IEntityTypeConfiguration<EventoSeguridad>
{
    public void Configure(EntityTypeBuilder<EventoSeguridad> entity)
    {
        entity.ToTable("Seguridad_Evento", "Seguridad");

        entity.HasKey(x => x.Evento_Seguridad_Codigo);

        entity.Property(x => x.Evento_Seguridad_Api_Codigo)
              .HasMaxLength(100)
              .IsRequired();

        entity.Property(x => x.Evento_Seguridad_Fecha)
              .HasDefaultValueSql("SYSDATETIME()");
    }
}
