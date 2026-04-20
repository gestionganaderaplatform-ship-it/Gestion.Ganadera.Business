using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Interfaces;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Infrastructure.Persistence.Repositories.Ganaderia;

public class RangoEdadRepository(AppDbContext context) : BaseRepository<RangoEdad>(context), IRangoEdadRepository
{
    public Task<bool> ExisteNombreAsync(
        string rangoEdadNombre,
        long? rangoEdadCodigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(item => item.Rango_Edad_Nombre == rangoEdadNombre);

        if (rangoEdadCodigoExcluir.HasValue)
        {
            query = query.Where(item => item.Rango_Edad_Codigo != rangoEdadCodigoExcluir.Value);
        }

        return query.AnyAsync(cancellationToken);
    }
}
