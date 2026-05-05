using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Interfaces;

public interface IMuerteRepository
{
    Task<bool> RegistrarAtomicoAsync(
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleMuerte detalle,
        Animal animalActualizado,
        CancellationToken cancellationToken = default);
}