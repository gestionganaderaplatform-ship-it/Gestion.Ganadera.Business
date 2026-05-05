using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Interfaces;

public interface IPesajeService
{
    Task<bool> CrearRegistroAsync(RegistrarPesajeRequest request, CancellationToken cancellationToken = default);
    Task<bool> RegistrarLoteAsync(RegistrarPesajeLoteRequest request, CancellationToken cancellationToken = default);
}