using Gestion.Ganadera.Business.API.ErrorHandling.Messages;
using Gestion.Ganadera.Business.API.Security.Sesiones;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Middleware
{
    public sealed class ValidacionSesionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(
            HttpContext context,
            IServicioValidacionSesionRemota servicioValidacionSesionRemota)
        {
            if (context.User.Identity?.IsAuthenticated != true)
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader)
                || string.IsNullOrWhiteSpace(authorizationHeader))
            {
                await _next(context);
                return;
            }

            var correlationId = context.Items.TryGetValue("X-Correlation-Id", out var cid)
                ? cid?.ToString()
                : null;

            var resultado = await servicioValidacionSesionRemota.ValidarAsync(
                authorizationHeader.ToString(),
                correlationId,
                context.RequestAborted);

            if (resultado is ResultadoValidacionSesionRemota.Activa
                or ResultadoValidacionSesionRemota.SinConfigurar)
            {
                await _next(context);
                return;
            }

            var (statusCode, title, detail) = resultado switch
            {
                ResultadoValidacionSesionRemota.Invalida => (
                    StatusCodes.Status401Unauthorized,
                    ApiErrorMessages.UnauthorizedDetailTitle,
                    "La sesion ya no esta disponible. Ingresa de nuevo."),
                _ => (
                    StatusCodes.Status503ServiceUnavailable,
                    ApiErrorMessages.InternalErrorTitle,
                    "No fue posible validar la sesion actual. Intenta nuevamente en un momento.")
            };

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = context.Request.Path
            };

            problem.Extensions["correlationId"] = correlationId;

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem, context.RequestAborted);
        }
    }
}
