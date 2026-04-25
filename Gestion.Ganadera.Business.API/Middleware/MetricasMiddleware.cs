using System.Diagnostics;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Observability.Interfaces;
using Gestion.Ganadera.Business.Application.Observability.ViewModels;

namespace Gestion.Ganadera.Business.API.Middleware
{
    /// <summary>
    /// Registra metricas basicas de cada request para monitoreo operativo del API.
    /// </summary>
    public class MetricasMiddleware(
        RequestDelegate next,
        ILogger<MetricasMiddleware> logger,
        IApiInfoProvider apiInfoProvider)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<MetricasMiddleware> _logger = logger;
        private readonly IApiInfoProvider _apiInfoProvider = apiInfoProvider;

        public async Task Invoke(
            HttpContext context,
            IRequestMetricasService requestMetricasService)
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
                    requestMetricasService,
                    stopwatch.ElapsedMilliseconds);
            }
        }

        private async Task GuardarMetricaAsync(
            HttpContext context,
            IRequestMetricasService requestMetricasService,
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

                await requestMetricasService.RegistrarAsync(metrica);
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
