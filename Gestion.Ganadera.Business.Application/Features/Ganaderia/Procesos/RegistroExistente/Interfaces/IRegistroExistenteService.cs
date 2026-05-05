using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Models;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;

public interface IRegistroExistenteService
{
    Task<bool> RegistrarAsync(RegistrarExistenteRequest request, CancellationToken cancellationToken = default);
Task<bool> RegistrarLoteAsync(RegistrarExistenteLoteRequest request, CancellationToken cancellationToken = default);
    Task<int> ObtenerSiguienteConsecutivoAsync(long fincaCodigo, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExistenciaIdentificador>> ExistenIdentificadoresAsync(long fincaCodigo, IEnumerable<string> identificadores, CancellationToken cancellationToken = default);
}
