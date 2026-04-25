using Gestion.Ganadera.Business.Application.Observability.ViewModels;

namespace Gestion.Ganadera.Business.Application.Observability.Interfaces
{
    /// <summary>
    /// Registra metricas tecnicas de requests sin exponer detalles de persistencia a la capa HTTP.
    /// </summary>
    public interface IRequestMetricasService
    {
        Task RegistrarAsync(MetricaSolicitudViewModel metrica);
    }
}
