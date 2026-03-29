using Gestion.Ganadera.Application.Observability.ViewModels;

namespace Gestion.Ganadera.Application.Observability.Interfaces
{
    /// <summary>
    /// Registra metricas tecnicas de requests sin exponer detalles de persistencia a la capa HTTP.
    /// </summary>
    public interface IRequestMetricsService
    {
        Task RegistrarAsync(MetricaSolicitudViewModel metrica);
    }
}
