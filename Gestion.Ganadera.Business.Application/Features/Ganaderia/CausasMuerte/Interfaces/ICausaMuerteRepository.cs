using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using CausaMuerteEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.CausaMuerte;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Interfaces;

public interface ICausaMuerteRepository : IBaseRepository<CausaMuerteEntity>
{
    Task<bool> ExisteNombreAsync(
        string causaMuerteNombre,
        long? causaMuerteCodigoExcluir = null,
        CancellationToken cancellationToken = default);
}
