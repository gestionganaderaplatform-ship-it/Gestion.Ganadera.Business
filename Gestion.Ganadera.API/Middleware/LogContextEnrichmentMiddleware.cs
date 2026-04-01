using System.Security.Claims;
using Serilog.Context;

namespace Gestion.Ganadera.API.Middleware
{
    /// <summary>
    /// Enriquce el contexto de log con usuario y cliente para trazabilidad SaaS.
    /// </summary>
    public sealed class LogContextEnrichmentMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            var usuarioSub =
                context.User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                context.User.FindFirstValue("sub");

            var clienteCodigo =
                context.User.FindFirstValue("cliente_codigo");

            using (LogContext.PushProperty("Log_Aplicacion_Usuario", usuarioSub ?? string.Empty))
            using (LogContext.PushProperty("Cliente_Codigo", ParseClienteCodigo(clienteCodigo)))
            {
                await _next(context);
            }
        }

        private static long? ParseClienteCodigo(string? clienteCodigo)
        {
            return long.TryParse(clienteCodigo, out var parsed)
                ? parsed
                : null;
        }
    }
}
