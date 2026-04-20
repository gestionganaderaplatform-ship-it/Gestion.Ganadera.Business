using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using System.Text.Json;
using Gestion.Ganadera.API.Middleware;
using Gestion.Ganadera.Application.Abstractions.Interfaces;

namespace Gestion.Ganadera.API.Extensions
{
    public static class ApiMiddlewareExtensions
    {
        public static WebApplication UseApiMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Test"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestion.Ganadera.API v1"));

                // Mantiene compatibilidad con rutas antiguas de OpenAPI usadas por el template.
                app.MapGet("/openapi/v1.json", () => Results.Redirect("/swagger/v1/swagger.json"))
                    .ExcludeFromDescription();
            }

            app.MapGet(
                    "/",
                    (IApiInfoProvider apiInfoProvider, IWebHostEnvironment environment) => Results.Ok(new
                    {
                        service = apiInfoProvider.ApiCodigo,
                        environment = environment.EnvironmentName,
                        status = "ok",
                        docs = "/swagger",
                        health = "/health",
                        ready = "/health/ready"
                    }))
                .AllowAnonymous()
                .ExcludeFromDescription();

            app.MapGet(
                    "/favicon.ico",
                    (IWebHostEnvironment environment) =>
                    {
                        var webRootPath = string.IsNullOrWhiteSpace(environment.WebRootPath)
                            ? Path.Combine(environment.ContentRootPath, "wwwroot")
                            : environment.WebRootPath;

                        var faviconPath = Path.Combine(webRootPath, "favicon.ico");

                        return System.IO.File.Exists(faviconPath)
                            ? Results.File(faviconPath, "image/x-icon")
                            : Results.NoContent();
                    })
                .AllowAnonymous()
                .ExcludeFromDescription();

            if (app.Configuration.GetValue<bool>("ForwardedHeaders:Enabled"))
            {
                app.UseForwardedHeaders();
            }

            app.UseHttpsRedirection();

            var corsSection = app.Configuration.GetSection("Cors");
            if (corsSection.GetValue<bool>("Enabled"))
            {
                var policyName = corsSection.GetValue<string>("PolicyName") ?? "DefaultCorsPolicy";
                app.UseCors(policyName);
            }

            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseAuthentication();
            app.UseMiddleware<ValidacionSesionMiddleware>();
            app.UseMiddleware<GanaderiaCatalogBootstrapMiddleware>();
            app.UseMiddleware<LogContextEnrichmentMiddleware>();
            app.UseAuthorization();
            app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            var healthOptions = new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(e => new
                        {
                            name = e.Key,
                            status = e.Value.Status.ToString(),
                            description = e.Value.Description
                        }),
                        duration = report.TotalDuration
                    };

                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(response, new JsonSerializerOptions
                        {
                            WriteIndented = true
                        }));
                }
            };

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("live"),
                ResponseWriter = healthOptions.ResponseWriter
            });

            var readinessEndpoint = app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("ready"),
                ResponseWriter = healthOptions.ResponseWriter
            });

            if (app.Configuration.GetValue<bool>("RateLimiting:Global:Enabled"))
            {
                readinessEndpoint.RequireRateLimiting("GlobalRateLimit");
            }

            return app;
        }
    }
}
