using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class CategoriaAnimalConfiguration : IEntityTypeConfiguration<CategoriaAnimal>
{
    public void Configure(EntityTypeBuilder<CategoriaAnimal> entity)
    {
        entity.ToTable("Categoria_Animal", "Ganaderia");

        entity.HasKey(x => x.Categoria_Animal_Codigo);

        entity.HasIndex(x => new { x.Cliente_Codigo, x.Categoria_Animal_Nombre })
            .IsUnique();

        entity.Property(x => x.Categoria_Animal_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Categoria_Animal_Nombre)
            .HasMaxLength(120)
            .IsRequired();

        entity.Property(x => x.Categoria_Animal_Sexo_Esperado)
            .HasMaxLength(20);

        entity.ConfigureAuditableGanaderia();
    }
}
