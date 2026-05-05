using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class CambioCategoriaSugeridoConfiguration : IEntityTypeConfiguration<CambioCategoriaSugerido>
{
    public void Configure(EntityTypeBuilder<CambioCategoriaSugerido> entity)
    {
        entity.ToTable("Cambio_Categoria_Sugerido", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Cambio_Categoria_Sugerido_Codigo);

        entity.Property(x => x.Sugerencia_Motivo)
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.Sugerencia_Estado)
            .HasMaxLength(30)
            .IsRequired();

        entity.HasOne(x => x.Animal)
            .WithMany()
            .HasForeignKey(x => x.Animal_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.CategoriaActual)
            .WithMany()
            .HasForeignKey(x => x.Categoria_Actual_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.CategoriaSugerida)
            .WithMany()
            .HasForeignKey(x => x.Categoria_Sugerida_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
            
        entity.HasIndex(x => new { x.Cliente_Codigo, x.Sugerencia_Estado });
    }
}
