namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Interfaces;

public interface ICambioCategoriaService
{
    /// <summary>
    /// Ejecuta el motor de evaluacion para generar sugerencias de cambio de categoria
    /// basadas en edad, peso y permanencia.
    /// </summary>
    Task<int> GenerarSugerenciasAsync(long fincaCodigo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtiene la lista de sugerencias pendientes para el cliente/finca actual.
    /// </summary>
    Task<IEnumerable<object>> ObtenerSugerenciasPendientesAsync(long fincaCodigo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Aprueba y ejecuta el cambio de categoria para un lote de sugerencias.
    /// </summary>
    Task<bool> AprobarSugerenciasAsync(IEnumerable<long> sugerenciasCodigos, CancellationToken cancellationToken = default);

    /// <summary>
    /// Rechaza o descarta sugerencias sugeridas por el sistema.
    /// </summary>
    Task<bool> RechazarSugerenciasAsync(IEnumerable<long> sugerenciasCodigos, CancellationToken cancellationToken = default);
}
