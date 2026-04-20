using Gestion.Ganadera.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Infrastructure.Persistence.Repositories.Ganaderia;

public class CategoriaAnimalRepository(AppDbContext context) : BaseRepository<CategoriaAnimal>(context), ICategoriaAnimalRepository
{
    public Task<bool> ExisteNombreAsync(
        string categoriaAnimalNombre,
        long? categoriaAnimalCodigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(item => item.Categoria_Animal_Nombre == categoriaAnimalNombre);

        if (categoriaAnimalCodigoExcluir.HasValue)
        {
            query = query.Where(item => item.Categoria_Animal_Codigo != categoriaAnimalCodigoExcluir.Value);
        }

        return query.AnyAsync(cancellationToken);
    }
}
