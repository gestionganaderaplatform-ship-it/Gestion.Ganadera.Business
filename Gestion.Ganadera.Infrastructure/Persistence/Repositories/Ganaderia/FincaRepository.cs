using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Infrastructure.Persistence.Repositories.Ganaderia;

public class FincaRepository(AppDbContext context) : BaseRepository<Finca>(context), IFincaRepository
{
    public Task<bool> ExisteNombreAsync(
        string fincaNombre,
        long? fincaCodigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(item => item.Finca_Nombre == fincaNombre);

        if (fincaCodigoExcluir.HasValue)
        {
            query = query.Where(item => item.Finca_Codigo != fincaCodigoExcluir.Value);
        }

        return query.AnyAsync(cancellationToken);
    }
}
