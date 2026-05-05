using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class VacunaEnfermedadConfiguration : IEntityTypeConfiguration<VacunaEnfermedad>
{
    public void Configure(EntityTypeBuilder<VacunaEnfermedad> entity)
    {
        entity.ToTable("Vacuna_Enfermedad", "Ganaderia");

        entity.HasKey(x => x.Vacuna_Enfermedad_Codigo);

        entity.HasIndex(x => new { x.Cliente_Codigo, x.Vacuna_Enfermedad_Nombre })
            .IsUnique();

        entity.Property(x => x.Vacuna_Enfermedad_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Vacuna_Enfermedad_Nombre)
            .HasMaxLength(80)
            .IsRequired();

        entity.ConfigureAuditableGanaderia();
    }
}

public sealed class VacunaConfiguration : IEntityTypeConfiguration<Vacuna>
{
    public void Configure(EntityTypeBuilder<Vacuna> entity)
    {
        entity.ToTable("Vacuna", "Ganaderia");

        entity.HasKey(x => x.Vacuna_Codigo);

        entity.HasIndex(x => new { x.Cliente_Codigo, x.Vacuna_Nombre })
            .IsUnique();

        entity.Property(x => x.Vacuna_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Vacuna_Nombre)
            .HasMaxLength(120)
            .IsRequired();

        entity.HasOne(x => x.Vacuna_Enfermedad)
            .WithMany()
            .HasForeignKey(x => x.Vacuna_Enfermedad_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.ConfigureAuditableGanaderia();
    }
}