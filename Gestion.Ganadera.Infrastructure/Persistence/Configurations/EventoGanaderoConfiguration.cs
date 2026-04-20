using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Infrastructure.Persistence.Configurations;

public sealed class EventoGanaderoConfiguration : IEntityTypeConfiguration<EventoGanadero>
{
    public void Configure(EntityTypeBuilder<EventoGanadero> entity)
    {
        entity.ToTable("Evento_Ganadero", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.HasIndex(x => new
        {
            x.Cliente_Codigo,
            x.Finca_Codigo,
            x.Evento_Ganadero_Tipo,
            x.Evento_Ganadero_Fecha
        });

        entity.Property(x => x.Evento_Ganadero_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Evento_Ganadero_Tipo)
            .HasMaxLength(60)
            .IsRequired();

        entity.Property(x => x.Evento_Ganadero_Registrado_Por)
            .HasMaxLength(200)
            .IsRequired();

        entity.Property(x => x.Evento_Ganadero_Estado)
            .HasMaxLength(40)
            .IsRequired();

        entity.Property(x => x.Evento_Ganadero_Observacion)
            .HasMaxLength(1000);

        entity.Property(x => x.Evento_Ganadero_Fecha_Registro)
            .HasDefaultValueSql("SYSDATETIME()");

        entity.HasOne<Finca>()
            .WithMany()
            .HasForeignKey(x => x.Finca_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<EventoGanadero>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Ganadero_Origen_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
