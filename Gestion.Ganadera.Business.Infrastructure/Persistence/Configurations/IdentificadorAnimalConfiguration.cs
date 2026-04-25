using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class IdentificadorAnimalConfiguration : IEntityTypeConfiguration<IdentificadorAnimal>
{
    public void Configure(EntityTypeBuilder<IdentificadorAnimal> entity)
    {
        entity.ToTable("Identificador_Animal", "Ganaderia");

        entity.HasKey(x => x.Identificador_Animal_Codigo);

        entity.HasIndex(x => new { x.Animal_Codigo, x.Tipo_Identificador_Codigo })
            .IsUnique();

        entity.HasIndex(x => new { x.Tipo_Identificador_Codigo, x.Identificador_Animal_Valor })
            .IsUnique();

        entity.HasIndex(x => x.Animal_Codigo)
            .IsUnique()
            .HasFilter("[Identificador_Animal_Es_Principal] = 1 AND [Identificador_Animal_Activo] = 1");

        entity.Property(x => x.Identificador_Animal_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Identificador_Animal_Valor)
            .HasMaxLength(120)
            .IsRequired();

        entity.ConfigureAuditableGanaderia();

        entity.HasOne<Animal>()
            .WithMany()
            .HasForeignKey(x => x.Animal_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<TipoIdentificador>()
            .WithMany()
            .HasForeignKey(x => x.Tipo_Identificador_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
