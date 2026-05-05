using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Interfaces;

public interface IDesteteRepository
{
    Task<bool> RegistrarAtomicoAsync(
        IEnumerable<EventoGanadero> eventos,
        IEnumerable<EventoGanaderoAnimal> eventosAnimal,
        IEnumerable<EventoDetalleDestete> detalles,
        CancellationToken cancellationToken = default);
}

public interface IDesteteService
{
    Task<bool> RegistrarAsync(RegistrarDesteteRequest request, CancellationToken cancellationToken = default);
    Task<bool> RegistrarLoteAsync(RegistrarDesteteLoteRequest request, CancellationToken cancellationToken = default);
}
