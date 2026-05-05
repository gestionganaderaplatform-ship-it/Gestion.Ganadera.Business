namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Interfaces;

public interface IValidarVacunacionRepository
{
    Task<bool> AnimalesExistenYActivosAsync(
        List<long> animalCodigos,
        CancellationToken cancellationToken = default);

    Task<bool> FincaValidaAsync(
        long fincaCodigo,
        CancellationToken cancellationToken = default);

    Task<bool> VacunaValidaAsync(
        long vacunaCodigo,
        CancellationToken cancellationToken = default);

    Task<bool> EnfermedadValidaAsync(
        long enfermedadCodigo,
        CancellationToken cancellationToken = default);
}