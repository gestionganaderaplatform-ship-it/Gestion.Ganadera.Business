using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia;

public class TratamientoProductoRepository(AppDbContext context) : BaseRepository<TratamientoProducto>(context), ITratamientoProductoRepository
{
    public Task<bool> ExisteNombreAsync(
        string nombre,
        long? codigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(item => item.Tratamiento_Producto_Nombre == nombre);

        if (codigoExcluir.HasValue)
        {
            query = query.Where(item => item.Tratamiento_Producto_Codigo != codigoExcluir.Value);
        }

        return query.AnyAsync(cancellationToken);
    }
}
