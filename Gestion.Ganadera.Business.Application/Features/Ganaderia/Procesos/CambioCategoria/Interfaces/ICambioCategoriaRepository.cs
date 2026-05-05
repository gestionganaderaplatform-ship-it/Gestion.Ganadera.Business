using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Interfaces;

public interface ICambioCategoriaRepository
{
    /// <summary>
    /// Obtiene animales activos que podrian ser candidatos a cambio de categoria.
    /// </summary>
    Task<IEnumerable<Animal>> ObtenerCandidatosCambioAsync(long fincaCodigo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene una sugerencia especifica con sus relaciones.
    /// </summary>
    Task<CambioCategoriaSugerido?> ObtenerSugerenciaPorCodigoAsync(long codigo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene todas las categorias con sus reglas de transicion.
    /// </summary>
    Task<IEnumerable<CategoriaAnimal>> ObtenerCategoriasConReglasAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Registra un lote de sugerencias de forma eficiente.
    /// </summary>
    Task<int> RegistrarSugerenciasMasivamenteAsync(IEnumerable<CambioCategoriaSugerido> sugerencias, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene sugerencias pendientes con detalles para la vista.
    /// </summary>
    Task<IEnumerable<SugerenciaCambioViewModel>> ObtenerSugerenciasPendientesDetalladasAsync(long fincaCodigo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ejecuta el cambio de categoria atomico para un animal.
    /// </summary>
    Task<bool> AplicarCambioCategoriaAtomicoAsync(
        Animal animal,
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleCambioCategoria detalle,
        CambioCategoriaSugerido sugerencia,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Marca sugerencias como rechazadas.
    /// </summary>
    Task<bool> RechazarSugerenciasAsync(IEnumerable<long> sugerenciasCodigos, CancellationToken cancellationToken = default);
}
