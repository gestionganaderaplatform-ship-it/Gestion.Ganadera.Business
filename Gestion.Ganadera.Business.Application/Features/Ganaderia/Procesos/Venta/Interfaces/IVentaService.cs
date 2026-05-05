using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Interfaces;

public interface IVentaService
{
    Task<bool> RegistrarAsync(RegistrarVentaRequest request, CancellationToken cancellationToken = default);
    Task<bool> RegistrarLoteAsync(RegistrarVentaLoteRequest request, CancellationToken cancellationToken = default);
}