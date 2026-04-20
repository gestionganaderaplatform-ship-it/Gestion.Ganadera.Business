using Gestion.Ganadera.Application.Features.Base.Interfaces;
using PotreroEntity = Gestion.Ganadera.Domain.Features.Ganaderia.Potrero;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Potreros.Interfaces;

public interface IPotreroRepository : IBaseRepository<PotreroEntity>
{
    Task<bool> ExisteNombreAsync(
        long fincaCodigo,
        string potreroNombre,
        long? potreroCodigoExcluir = null,
        CancellationToken cancellationToken = default);
}
