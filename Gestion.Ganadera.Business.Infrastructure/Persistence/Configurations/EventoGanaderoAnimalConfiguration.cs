using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class EventoGanaderoAnimalConfiguration : IEntityTypeConfiguration<EventoGanaderoAnimal>
{
    public void Configure(EntityTypeBuilder<EventoGanaderoAnimal> entity)
    {
        entity.ToTable("Evento_Ganadero_Animal", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Animal_Codigo);

        entity.HasIndex(x => new { x.Evento_Ganadero_Codigo, x.Animal_Codigo })
            .IsUnique();

        entity.HasIndex(x => x.Animal_Codigo);

        entity.Property(x => x.Evento_Ganadero_Animal_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Evento_Ganadero_Animal_Estado_Afectacion)
            .HasMaxLength(40)
            .IsRequired();

        entity.Property(x => x.Evento_Ganadero_Animal_Observacion)
            .HasMaxLength(500);

        entity.HasOne<EventoGanadero>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<Animal>()
            .WithMany()
            .HasForeignKey(x => x.Animal_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
