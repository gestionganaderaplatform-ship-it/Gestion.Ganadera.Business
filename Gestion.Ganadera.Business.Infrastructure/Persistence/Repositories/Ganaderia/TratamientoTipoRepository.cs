using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia;

public class TratamientoTipoRepository(AppDbContext context) : BaseRepository<TratamientoTipo>(context), ITratamientoTipoRepository
{
    public Task<bool> ExisteNombreAsync(
        string nombre,
        long? codigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(item => item.Tratamiento_Tipo_Nombre == nombre);

        if (codigoExcluir.HasValue)
        {
            query = query.Where(item => item.Tratamiento_Tipo_Codigo != codigoExcluir.Value);
        }

        return query.AnyAsync(cancellationToken);
    }
}
