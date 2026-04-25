using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using FincaEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.Finca;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.Interfaces;

public interface IFincaRepository : IBaseRepository<FincaEntity>
{
    Task<bool> ExisteNombreAsync(
        string fincaNombre,
        long? fincaCodigoExcluir = null,
        CancellationToken cancellationToken = default);
}
