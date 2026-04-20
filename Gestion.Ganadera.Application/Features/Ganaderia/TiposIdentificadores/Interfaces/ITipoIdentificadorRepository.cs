using Gestion.Ganadera.Application.Features.Base.Interfaces;
using TipoIdentificadorEntity = Gestion.Ganadera.Domain.Features.Ganaderia.TipoIdentificador;

namespace Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;

public interface ITipoIdentificadorRepository : IBaseRepository<TipoIdentificadorEntity>
{
    Task<bool> ExisteNombreAsync(
        string tipoIdentificadorNombre,
        long? tipoIdentificadorCodigoExcluir = null,
        CancellationToken cancellationToken = default);
}
