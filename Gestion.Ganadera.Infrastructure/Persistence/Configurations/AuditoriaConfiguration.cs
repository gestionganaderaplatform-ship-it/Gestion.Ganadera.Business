using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gestion.Ganadera.Domain.Features.Seguridad;



namespace Gestion.Ganadera.Infrastructure.Persistence.Configurations;

public class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
{
    public void Configure(EntityTypeBuilder<Auditoria> entity)
    {
        entity.ToTable("Auditoria", "Seguridad");

        entity.HasKey(x => x.Auditoria_Codigo);

        entity.Property(x => x.Auditoria_Api_Codigo)
              .HasMaxLength(100)
              .IsRequired();

        entity.Property(x => x.Auditoria_Nombre_Tabla)
              .HasMaxLength(150)
              .IsRequired();

        entity.Property(x => x.Auditoria_Valor_Clave)
              .HasMaxLength(200)
              .IsRequired();

        entity.Property(x => x.Auditoria_Modificado_Por)
              .HasMaxLength(200)
              .IsRequired();

        entity.Property(x => x.Cliente_Codigo);

        entity.Property(x => x.Auditoria_Codigo)
              .ValueGeneratedOnAdd();

        entity.HasIndex(x => x.Cliente_Codigo);
    }
}
