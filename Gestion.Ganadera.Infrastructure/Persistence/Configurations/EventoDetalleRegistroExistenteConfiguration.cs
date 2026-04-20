using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Infrastructure.Persistence.Configurations;

public sealed class EventoDetalleRegistroExistenteConfiguration : IEntityTypeConfiguration<EventoDetalleRegistroExistente>
{
    public void Configure(EntityTypeBuilder<EventoDetalleRegistroExistente> entity)
    {
        entity.ToTable("Evento_Detalle_Registro_Existente", "Ganaderia");

        entity.ConfigureAuditableGanaderia();

        entity.HasKey(x => x.Evento_Ganadero_Codigo);

        entity.Property(x => x.Evento_Detalle_Registro_Existente_Identificador_Valor)
            .HasMaxLength(120)
            .IsRequired();

        entity.Property(x => x.Evento_Detalle_Registro_Existente_Sexo)
            .HasMaxLength(20)
            .IsRequired();

        entity.HasOne<EventoGanadero>()
            .WithMany()
            .HasForeignKey(x => x.Evento_Ganadero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<TipoIdentificador>()
            .WithMany()
            .HasForeignKey(x => x.Tipo_Identificador_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<CategoriaAnimal>()
            .WithMany()
            .HasForeignKey(x => x.Categoria_Animal_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<RangoEdad>()
            .WithMany()
            .HasForeignKey(x => x.Rango_Edad_Codigo)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne<Potrero>()
            .WithMany()
            .HasForeignKey(x => x.Potrero_Codigo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
