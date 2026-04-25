using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion.Ganadera.Business.API.Extensions;
using Gestion.Ganadera.Business.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Registra la infraestructura transversal y los servicios base del host.
builder.AddApiServices();
builder.AddForwardedHeadersSupport();
builder.AddRateLimiting();
builder.AddAutoMapper();
builder.AddOpenApiServices();
builder.AddControllers();
builder.AddApiVersioningConfig();
builder.AddSqlServerDatabase<AppDbContext>();
builder.AddLogging();
builder.AddProjectDependencyInjection();
builder.AddHealthChecksServices();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;
});

// Permite centralizar la respuesta de validacion en FluentValidation y ProblemDetails.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.AddAuthorizationServices();
builder.AddJwtAuthentication();
builder.AddCorsServices();

var app = builder.Build();
var globalRateLimitEnabled = builder.Configuration.GetValue<bool>("RateLimiting:Global:Enabled");

// El orden del pipeline garantiza trazabilidad, proteccion y observabilidad consistentes.
app.UseApiMiddlewares();

app.UseRequestMetrics();

if (globalRateLimitEnabled)
{
    app.UseRateLimiting();
}

var controllers = app.MapControllers();
if (globalRateLimitEnabled)
{
    controllers.RequireRateLimiting("GlobalRateLimit");
}

await app.ApplySafeDevelopmentMigrationsAsync<AppDbContext>();

app.Run();
