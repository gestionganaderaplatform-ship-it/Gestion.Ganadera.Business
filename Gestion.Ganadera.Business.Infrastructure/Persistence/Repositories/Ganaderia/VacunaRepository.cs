using Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia;

public class VacunaRepository(AppDbContext context) : BaseRepository<Vacuna>(context), IVacunaRepository
{
    public Task<bool> ExisteNombreAsync(
        string vacunaNombre,
        long? vacunaCodigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(item => item.Vacuna_Nombre == vacunaNombre);

        if (vacunaCodigoExcluir.HasValue)
        {
            query = query.Where(item => item.Vacuna_Codigo != vacunaCodigoExcluir.Value);
        }

        return query.AnyAsync(cancellationToken);
    }
}
