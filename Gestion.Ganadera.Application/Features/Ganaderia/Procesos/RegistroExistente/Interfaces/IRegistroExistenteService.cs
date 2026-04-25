using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;

public interface IRegistroExistenteService
{
    Task<bool> RegistrarAsync(RegistrarExistenteRequest request, CancellationToken cancellationToken = default);
    Task<bool> RegistrarLoteAsync(RegistrarExistenteLoteRequest request, CancellationToken cancellationToken = default);
    Task<bool> ExisteIdentificadorAsync(long fincaCodigo, string identificador, CancellationToken cancellationToken = default);
    Task<int> ObtenerSiguienteConsecutivoAsync(long fincaCodigo, CancellationToken cancellationToken = default);
}
