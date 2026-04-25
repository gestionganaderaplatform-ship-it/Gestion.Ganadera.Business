namespace Gestion.Ganadera.Business.API.Extensions
{
    /// <summary>
/// Inserta en el pipeline el middleware de rate limiting configurado por el host.
/// </summary>
public static class RateLimitingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimiting(
            this IApplicationBuilder app)
        {
            return app.UseRateLimiter();
        }
    }
}

