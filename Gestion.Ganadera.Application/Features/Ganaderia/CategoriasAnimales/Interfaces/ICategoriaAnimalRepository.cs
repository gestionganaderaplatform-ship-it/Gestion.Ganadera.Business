using Gestion.Ganadera.Application.Features.Base.Interfaces;
using CategoriaAnimalEntity = Gestion.Ganadera.Domain.Features.Ganaderia.CategoriaAnimal;

namespace Gestion.Ganadera.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;

public interface ICategoriaAnimalRepository : IBaseRepository<CategoriaAnimalEntity>
{
    Task<bool> ExisteNombreAsync(
        string categoriaAnimalNombre,
        long? categoriaAnimalCodigoExcluir = null,
        CancellationToken cancellationToken = default);
}
