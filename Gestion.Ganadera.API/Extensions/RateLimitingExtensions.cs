using Microsoft.AspNetCore.Mvc;
using System.Threading.RateLimiting;
using Gestion.Ganadera.API.Constants;
using Gestion.Ganadera.API.ErrorHandling.Messages;
using Gestion.Ganadera.Application.Observability.Interfaces;
using Gestion.Ganadera.Application.Observability.ViewModels;

namespace Gestion.Ganadera.API.Extensions
{
    /// <summary>
    /// Configura politicas de rate limiting y la respuesta estandar para rechazos.
    /// </summary>
    public static class RateLimitingExtensions
    {
        public static WebApplicationBuilder AddRateLimiting(
            this WebApplicationBuilder builder)
        {
            var rateLimitConfig = builder.Configuration.GetSection("RateLimiting:Global");
            if (!rateLimitConfig.GetValue<bool>("Enabled"))
            {
                return builder;
            }

            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.OnRejected = async (context, token) =>
                {
                    var httpContext = context.HttpContext;

                    try
                    {
                        var securityEventService =
                            httpContext.RequestServices.GetRequiredService<ISecurityEventService>();

                        await securityEventService.RegistrarAsync(new EventoSeguridadViewModel
                        {
                            Evento_Seguridad_Fecha = DateTime.UtcNow,
                            Evento_Seguridad_Tipo_Evento = SecurityEventTypes.RateLimitExceeded,
                            Evento_Seguridad_Ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                            Evento_Seguridad_Endpoint = httpContext.Request.Path,
                            Evento_Seguridad_Origin = httpContext.Request.Headers.Origin,
                            Evento_Seguridad_UserAgent = httpContext.Request.Headers.UserAgent,
                            Evento_Seguridad_CorrelationId = httpContext.Items["X-Correlation-Id"]?.ToString()
                        });
                    }
                    catch
                    {
                    }

                    var problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status429TooManyRequests,
                        Title = ApiErrorMessages.RateLimitExceededTitle,
                        Detail = ApiErrorMessages.RateLimitExceededDetail,
                        Instance = httpContext.Request.Path
                    };

                    problem.Extensions["correlationId"] =
                        httpContext.Items.TryGetValue("X-Correlation-Id", out var cid)
                            ? cid
                            : null;

                    httpContext.Response.ContentType = "application/problem+json";
                    await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken: token);
                };

                var permitLimit = rateLimitConfig.GetValue<int>("PermitLimit");
                var windowSeconds = rateLimitConfig.GetValue<int>("WindowSeconds");
                var queueLimit = rateLimitConfig.GetValue<int>("QueueLimit");

                options.AddPolicy("GlobalRateLimit", httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = permitLimit,
                            Window = TimeSpan.FromSeconds(windowSeconds),
                            QueueLimit = queueLimit
                        }));
            });

            return builder;
        }
    }
}
