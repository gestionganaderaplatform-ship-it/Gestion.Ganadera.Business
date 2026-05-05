using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Interfaces;

public interface IPesajeRepository
{
    Task<bool> CrearRegistroAtomicoAsync(
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetallePesaje detalle,
        Animal animalActualizado,
        CancellationToken cancellationToken = default);

    Task<bool> RegistrarLoteAtomicoAsync(
        IEnumerable<(EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetallePesaje Detalle, Animal AnimalActualizado)> lote,
        CancellationToken cancellationToken = default);
}