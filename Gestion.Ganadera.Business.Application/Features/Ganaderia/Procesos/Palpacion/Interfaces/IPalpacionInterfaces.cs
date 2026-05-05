using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Interfaces;

public interface IPalpacionRepository
{
    Task<bool> RegistrarAtomicoAsync(
        IEnumerable<EventoGanadero> eventos,
        IEnumerable<EventoGanaderoAnimal> eventosAnimal,
        IEnumerable<EventoDetallePalpacion> detalles,
        CancellationToken cancellationToken = default);
}

public interface IPalpacionService
{
    Task<bool> RegistrarAsync(RegistrarPalpacionRequest request, CancellationToken cancellationToken = default);
    Task<bool> RegistrarLoteAsync(RegistrarPalpacionLoteRequest request, CancellationToken cancellationToken = default);
}
