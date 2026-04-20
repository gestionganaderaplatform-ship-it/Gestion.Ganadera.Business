using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Infrastructure.Persistence.Repositories.Ganaderia;

public class TipoIdentificadorRepository(AppDbContext context) : BaseRepository<TipoIdentificador>(context), ITipoIdentificadorRepository
{
    public Task<bool> ExisteNombreAsync(
        string tipoIdentificadorNombre,
        long? tipoIdentificadorCodigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(item => item.Tipo_Identificador_Nombre == tipoIdentificadorNombre);

        if (tipoIdentificadorCodigoExcluir.HasValue)
        {
            query = query.Where(item => item.Tipo_Identificador_Codigo != tipoIdentificadorCodigoExcluir.Value);
        }

        return query.AnyAsync(cancellationToken);
    }
}
