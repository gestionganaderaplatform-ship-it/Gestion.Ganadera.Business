using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> entity)
    {
        entity.ToTable("Animal", "Ganaderia");

        entity.HasKey(x => x.Animal_Codigo);

        entity.HasIndex(x => new { x.Cliente_Codigo, x.Animal_Activo });

        entity.HasIndex(x => x.Finca_Codigo);
        entity.HasIndex(x => x.Potrero_Codigo);
        entity.HasIndex(x => x.Categoria_Animal_Codigo);

        entity.Property(x => x.Animal_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Animal_Sexo)
            .HasMaxLength(20)
            .IsRequired();

        entity.Property(x => x.Animal_Origen_Ingreso)
            .HasMaxLength(40)
            .IsRequired();

        entity.ConfigureAuditableGanaderia();

        entity.Property(x => x.Animal_Fecha_Registro_Ingreso)
            .HasDefaultValueSql("SYSDATETIME()");

        entity.HasOne(x => x.Finca)
            .WithMany()
            .HasForeignKey(x => x.Finca_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Potrero)
            .WithMany()
            .HasForeignKey(x => x.Potrero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Categoria)
            .WithMany()
            .HasForeignKey(x => x.Categoria_Animal_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
