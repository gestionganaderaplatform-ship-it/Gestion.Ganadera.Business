using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia;

public class PalpacionResultadoRepository(AppDbContext context)
    : BaseRepository<PalpacionResultado>(context), IPalpacionResultadoRepository
{
    public async Task<bool> ExisteNombreAsync(
        string nombre,
        long? codigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = context.PalpacionesResultados
            .Where(x => x.Palpacion_Resultado_Nombre.ToLower() == nombre.Trim().ToLower());

        if (codigoExcluir.HasValue)
        {
            query = query.Where(x => x.Palpacion_Resultado_Codigo != codigoExcluir.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
}
