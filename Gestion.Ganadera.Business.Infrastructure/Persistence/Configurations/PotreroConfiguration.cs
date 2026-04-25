using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class PotreroConfiguration : IEntityTypeConfiguration<Potrero>
{
    public void Configure(EntityTypeBuilder<Potrero> entity)
    {
        entity.ToTable("Potrero", "Ganaderia");

        entity.HasKey(x => x.Potrero_Codigo);

        entity.HasIndex(x => new { x.Finca_Codigo, x.Potrero_Nombre })
            .IsUnique();

        entity.Property(x => x.Potrero_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Potrero_Nombre)
            .HasMaxLength(200)
            .IsRequired();

        entity.ConfigureAuditableGanaderia();

        entity.HasOne<Finca>()
            .WithMany()
            .HasForeignKey(x => x.Finca_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
