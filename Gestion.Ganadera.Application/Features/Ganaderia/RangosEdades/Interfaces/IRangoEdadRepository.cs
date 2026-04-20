using Gestion.Ganadera.Application.Features.Base.Interfaces;
using RangoEdadEntity = Gestion.Ganadera.Domain.Features.Ganaderia.RangoEdad;

namespace Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Interfaces;

public interface IRangoEdadRepository : IBaseRepository<RangoEdadEntity>
{
    Task<bool> ExisteNombreAsync(
        string rangoEdadNombre,
        long? rangoEdadCodigoExcluir = null,
        CancellationToken cancellationToken = default);
}
