using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Interfaces;

public interface ITrasladoFincaRepository
{
    Task<bool> RegistrarAtomicoAsync(
        IEnumerable<EventoGanadero> eventos,
        IEnumerable<EventoGanaderoAnimal> eventosAnimal,
        IEnumerable<Animal> animalesActualizados,
        IEnumerable<EventoDetalleTrasladoFinca> detalles,
        CancellationToken cancellationToken = default);
}
