using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia;

public class CausaMuerteRepository(
    AppDbContext context)
    : BaseRepository<CausaMuerte>(context), ICausaMuerteRepository
{
    public async Task<bool> ExisteNombreAsync(
        string causaMuerteNombre,
        long? causaMuerteCodigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(x => x.Causa_Muerte_Nombre == causaMuerteNombre);

        if (causaMuerteCodigoExcluir.HasValue)
        {
            query = query.Where(x => x.Causa_Muerte_Codigo != causaMuerteCodigoExcluir.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
}
