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

        entity.Property(x => x.Auditoria_Codigo)
              .ValueGeneratedOnAdd();
    }
}
