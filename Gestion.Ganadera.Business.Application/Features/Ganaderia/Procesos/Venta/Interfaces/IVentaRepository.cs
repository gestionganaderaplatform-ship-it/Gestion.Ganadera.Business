using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Interfaces;

public interface IVentaRepository
{
    Task<bool> RegistrarAtomicoAsync(
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleVenta detalle,
        Animal animalActualizado,
        CancellationToken cancellationToken = default);

    Task<bool> RegistrarLoteAtomicoAsync(
        IEnumerable<(EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleVenta Detalle, Animal AnimalActualizado)> lote,
        CancellationToken cancellationToken = default);
}