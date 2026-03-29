using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using Gestion.Ganadera.API.Middleware;

namespace Gestion.Ganadera.API.Extensions
{
    public static class ApiMiddlewareExtensions
    {
        public static WebApplication UseApiMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestion.Ganadera.API v1"));

                // Mantiene compatibilidad con rutas antiguas de OpenAPI usadas por el template.
                app.MapGet("/openapi/v1.json", () => Results.Redirect("/swagger/v1/swagger.json"))
                    .ExcludeFromDescription();
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
            app.UseAuthorization();
            app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.MapHealthChecks("/health", new HealthCheckOptions
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
            });

            return app;
        }
    }
}
