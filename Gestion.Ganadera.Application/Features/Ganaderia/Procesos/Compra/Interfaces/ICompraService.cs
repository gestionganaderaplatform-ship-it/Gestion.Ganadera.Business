using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.Compra.Models;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Procesos.Compra.Interfaces;

public interface ICompraService
{
    Task<bool> CrearRegistroAsync(RegistrarCompraRequest request, CancellationToken cancellationToken = default);
}
