using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using TipoIdentificadorEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.TipoIdentificador;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;

public interface ITipoIdentificadorRepository : IBaseRepository<TipoIdentificadorEntity>
{
    Task<bool> ExisteNombreAsync(
        string tipoIdentificadorNombre,
        long? tipoIdentificadorCodigoExcluir = null,
        CancellationToken cancellationToken = default);
}
