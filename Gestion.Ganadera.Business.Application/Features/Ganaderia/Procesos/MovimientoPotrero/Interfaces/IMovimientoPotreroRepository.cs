using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Interfaces;

public interface IMovimientoPotreroRepository
{
    Task<bool> RegistrarAtomicoAsync(
        IEnumerable<EventoGanadero> eventos,
        IEnumerable<EventoGanaderoAnimal> eventosAnimal,
        IEnumerable<Animal> animalesActualizados,
        IEnumerable<EventoDetalleMovimientoPotrero> detalles,
        CancellationToken cancellationToken = default);
}