using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class TratamientoTipoConfiguration : IEntityTypeConfiguration<TratamientoTipo>
{
    public void Configure(EntityTypeBuilder<TratamientoTipo> entity)
    {
        entity.ToTable("Tratamiento_Tipo", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Tratamiento_Tipo_Codigo);

        entity.Property(x => x.Tratamiento_Tipo_Nombre)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(x => x.Tratamiento_Tipo_Activa)
            .HasDefaultValue(true);
    }
}

public sealed class TratamientoProductoConfiguration : IEntityTypeConfiguration<TratamientoProducto>
{
    public void Configure(EntityTypeBuilder<TratamientoProducto> entity)
    {
        entity.ToTable("Tratamiento_Producto", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Tratamiento_Producto_Codigo);

        entity.Property(x => x.Tratamiento_Producto_Nombre)
            .IsRequired()
            .HasMaxLength(150);

        entity.Property(x => x.Tratamiento_Producto_Activo)
            .HasDefaultValue(true);

        entity.HasOne(x => x.Tratamiento_Tipo)
            .WithMany()
            .HasForeignKey(x => x.Tratamiento_Tipo_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
