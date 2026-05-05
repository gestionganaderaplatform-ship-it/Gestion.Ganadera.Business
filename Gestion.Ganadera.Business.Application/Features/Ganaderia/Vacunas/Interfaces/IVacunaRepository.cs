using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using VacunaEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.Vacuna;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Interfaces;

public interface IVacunaRepository : IBaseRepository<VacunaEntity>
{
    Task<bool> ExisteNombreAsync(
        string vacunaNombre,
        long? vacunaCodigoExcluir = null,
        CancellationToken cancellationToken = default);
}