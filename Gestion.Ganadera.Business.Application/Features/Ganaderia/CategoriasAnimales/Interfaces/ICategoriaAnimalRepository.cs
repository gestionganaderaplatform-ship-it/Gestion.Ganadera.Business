using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using CategoriaAnimalEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.CategoriaAnimal;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;

public interface ICategoriaAnimalRepository : IBaseRepository<CategoriaAnimalEntity>
{
    Task<bool> ExisteNombreAsync(
        string categoriaAnimalNombre,
        long? categoriaAnimalCodigoExcluir = null,
        CancellationToken cancellationToken = default);

    Task<bool> EsCompatibleConSexoAsync(
        long categoriaAnimalCodigo,
        string animalSexo,
        CancellationToken cancellationToken = default);
}
