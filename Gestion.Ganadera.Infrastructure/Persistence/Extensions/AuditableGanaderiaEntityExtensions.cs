using Gestion.Ganadera.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion.Ganadera.Infrastructure.Persistence.Extensions;

internal static class AuditableGanaderiaEntityExtensions
{
    public static void ConfigureAuditableGanaderia<TEntity>(this EntityTypeBuilder<TEntity> entity)
        where TEntity : AuditableEntity
    {
        entity.Ignore(x => x.Codigo_Publico);

        entity.Property(x => x.Fecha_Creado)
            .HasDefaultValueSql("SYSDATETIME()");

        entity.Property(x => x.Creado_Por)
            .IsRequired();
    }
}
