using System.Diagnostics;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Observability.Interfaces;
using Gestion.Ganadera.Application.Observability.ViewModels;

namespace Gestion.Ganadera.API.Middleware
{
    /// <summary>
    /// Registra metricas basicas de cada request para monitoreo operativo del API.
    /// </summary>
    public class MetricsMiddleware(
        RequestDelegate next,
        ILogger<MetricsMiddleware> logger,
        IApiInfoProvider apiInfoProvider)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<MetricsMiddleware> _logger = logger;
        private readonly IApiInfoProvider _apiInfoProvider = apiInfoProvider;

        public async Task Invoke(
            HttpContext context,
            IRequestMetricsService requestMetricsService)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();
                await GuardarMetricaAsync(
                    context,
                    requestMetricsService,
                    stopwatch.ElapsedMilliseconds);
            }
        }

        private async Task GuardarMetricaAsync(
            HttpContext context,
            IRequestMetricsService requestMetricsService,
            long tiempoRespuestaMs)
        {
            try
            {
                var correlationId =
                    context.Items.TryGetValue("X-Correlation-Id", out var cid)
                        ? cid?.ToString()
                        : null;

                var metrica = new MetricaSolicitudViewModel
                {
                    Metrica_Solicitud_Api_Codigo = _apiInfoProvider.ApiCodigo,
                    Metrica_Solicitud_Metodo_Http = context.Request.Method,
                    Metrica_Solicitud_Ruta_Request = context.Request.Path.Value ?? string.Empty,
                    Metrica_Solicitud_Codigo_Estado = context.Response.StatusCode,
                    Metrica_Solicitud_Tiempo_Respuesta_Ms = tiempoRespuestaMs,
                    Metrica_Solicitud_Correlation_Id = correlationId,
                    Metrica_Solicitud_Fecha_Creacion = DateTime.Now
                };

                await requestMetricsService.RegistrarAsync(metrica);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error al guardar metricas de request {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);
            }
        }
    }
}
