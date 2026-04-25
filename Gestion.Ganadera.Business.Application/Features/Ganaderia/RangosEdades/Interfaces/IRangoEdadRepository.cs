using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using RangoEdadEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.RangoEdad;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.RangosEdades.Interfaces;

public interface IRangoEdadRepository : IBaseRepository<RangoEdadEntity>
{
    Task<bool> ExisteNombreAsync(
        string rangoEdadNombre,
        long? rangoEdadCodigoExcluir = null,
        CancellationToken cancellationToken = default);
}
