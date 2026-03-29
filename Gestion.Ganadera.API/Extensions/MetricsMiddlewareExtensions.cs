using Gestion.Ganadera.API.Middleware;

namespace Gestion.Ganadera.API.Extensions
{
    /// <summary>
/// Agrega el middleware que registra metricas operativas por request.
/// </summary>
public static class MetricsMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMetrics(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<MetricsMiddleware>();
        }
    }
}

