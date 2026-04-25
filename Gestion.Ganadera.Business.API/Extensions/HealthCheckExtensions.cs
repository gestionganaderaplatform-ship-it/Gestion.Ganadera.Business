using Microsoft.Extensions.Diagnostics.HealthChecks;
using Gestion.Ganadera.Business.API.Requests.Messages;

namespace Gestion.Ganadera.Business.API.Extensions
{
    /// <summary>
    /// Registra health checks del API y sus dependencias criticas.
    /// </summary>
    public static class HealthCheckExtensions
    {
        public static WebApplicationBuilder AddHealthChecksServices(
            this WebApplicationBuilder builder)
        {
            var connectionString =
                builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    RequestMessages.DefaultConnectionMissing);

            builder.Services
                .AddHealthChecks()
                .AddCheck(
                    name: "self",
                    check: () => HealthCheckResult.Healthy("API activa."),
                    tags: ["live"])
                .AddSqlServer(
                    connectionString: connectionString,
                    name: "sqlserver",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: ["ready"]
                );

            return builder;
        }
    }
}
