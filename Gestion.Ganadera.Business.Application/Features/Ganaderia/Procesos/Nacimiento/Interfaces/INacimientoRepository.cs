using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Interfaces;

public interface INacimientoRepository
{
    Task<bool> RegistrarAtomicoAsync(
        Animal cria,
        IdentificadorAnimal identificador,
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleNacimiento detalle,
        AnimalRelacionFamiliar relacion,
        CancellationToken cancellationToken = default);

    Task<bool> MadreExisteEnFincaAsync(
        long fincaCodigo,
        long madreAnimalCodigo,
        CancellationToken cancellationToken = default);

    Task<bool> MadreElegibleEnFincaAsync(
        long fincaCodigo,
        long madreAnimalCodigo,
        CancellationToken cancellationToken = default);

    Task<int> ObtenerSiguienteConsecutivoAsync(
        long fincaCodigo,
        CancellationToken cancellationToken = default);
}
