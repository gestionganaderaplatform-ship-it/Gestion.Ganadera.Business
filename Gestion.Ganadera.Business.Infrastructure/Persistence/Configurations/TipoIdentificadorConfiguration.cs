using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public sealed class TipoIdentificadorConfiguration : IEntityTypeConfiguration<TipoIdentificador>
{
    public void Configure(EntityTypeBuilder<TipoIdentificador> entity)
    {
        entity.ToTable("Tipo_Identificador", "Ganaderia");

        entity.HasKey(x => x.Tipo_Identificador_Codigo);

        entity.HasIndex(x => new { x.Cliente_Codigo, x.Tipo_Identificador_Nombre })
            .IsUnique();

        entity.Property(x => x.Tipo_Identificador_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Tipo_Identificador_Nombre)
            .HasMaxLength(120)
            .IsRequired();

        entity.Property(x => x.Tipo_Identificador_Codigo_Interno)
            .HasMaxLength(60);

        entity.ConfigureAuditableGanaderia();
    }
}
