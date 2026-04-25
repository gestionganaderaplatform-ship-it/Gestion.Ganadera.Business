using Gestion.Ganadera.Business.Domain.Features.Navegacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Configurations;

public class MenuNavegacionConfiguration : IEntityTypeConfiguration<MenuNavegacion>
{
    public void Configure(EntityTypeBuilder<MenuNavegacion> entity)
    {
        entity.ToTable("Menu_Navegacion", "Aplicacion");

        entity.HasKey(x => x.Menu_Navegacion_Codigo);

        entity.Property(x => x.Menu_Navegacion_Codigo)
            .ValueGeneratedOnAdd();

        entity.Property(x => x.Menu_Navegacion_Clave)
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.Menu_Navegacion_Titulo)
            .HasMaxLength(150)
            .IsRequired();

        entity.Property(x => x.Menu_Navegacion_Icono)
            .HasMaxLength(100)
            .IsRequired();

        entity.Property(x => x.Menu_Navegacion_Tipo)
            .HasMaxLength(30)
            .IsRequired();

        entity.Property(x => x.Menu_Navegacion_Ruta)
            .HasMaxLength(250);

        entity.Property(x => x.Menu_Navegacion_Accion)
            .HasMaxLength(50);

        entity.Property(x => x.Menu_Navegacion_Permiso_Requerido)
            .HasMaxLength(150);

        entity.HasIndex(x => x.Menu_Navegacion_Clave)
            .IsUnique();

        entity.HasIndex(x => new
        {
            x.Menu_Navegacion_Padre_Codigo,
            x.Menu_Navegacion_Orden
        });

        entity.HasOne<MenuNavegacion>()
            .WithMany()
            .HasForeignKey(x => x.Menu_Navegacion_Padre_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
