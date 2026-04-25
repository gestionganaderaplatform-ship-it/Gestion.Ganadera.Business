using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using PotreroEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.Potrero;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.Interfaces;

public interface IPotreroRepository : IBaseRepository<PotreroEntity>
{
    Task<bool> ExisteNombreAsync(
        long fincaCodigo,
        string potreroNombre,
        long? potreroCodigoExcluir = null,
        CancellationToken cancellationToken = default);
}
