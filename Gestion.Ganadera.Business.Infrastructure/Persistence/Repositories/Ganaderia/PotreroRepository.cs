using Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia;

public class PotreroRepository(AppDbContext context) : BaseRepository<Potrero>(context), IPotreroRepository
{
    public Task<bool> ExisteNombreAsync(
        long fincaCodigo,
        string potreroNombre,
        long? potreroCodigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(item => item.Finca_Codigo == fincaCodigo && item.Potrero_Nombre == potreroNombre);

        if (potreroCodigoExcluir.HasValue)
        {
            query = query.Where(item => item.Potrero_Codigo != potreroCodigoExcluir.Value);
        }

        return query.AnyAsync(cancellationToken);
    }
}
