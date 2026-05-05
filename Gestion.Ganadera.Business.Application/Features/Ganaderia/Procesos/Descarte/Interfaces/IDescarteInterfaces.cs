using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Interfaces;

public interface IDescarteRepository
{
    Task<bool> RegistrarAtomicoAsync(
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleDescarte detalle,
        Animal animalActualizado,
        CancellationToken cancellationToken = default);

    Task<bool> RegistrarLoteAtomicoAsync(
        IEnumerable<(EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleDescarte Detalle, Animal AnimalActualizado)> lote,
        CancellationToken cancellationToken = default);
}

public interface IDescarteService
{
    Task<bool> RegistrarAsync(RegistrarDescarteRequest request, CancellationToken cancellationToken = default);
    Task<bool> RegistrarLoteAsync(RegistrarDescarteLoteRequest request, CancellationToken cancellationToken = default);
}
