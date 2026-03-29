using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion.Ganadera.API.Extensions;
using Gestion.Ganadera.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Registra la infraestructura transversal y los servicios base del host.
builder.AddApiServices();
builder.AddLogging();
builder.AddRateLimiting();
builder.AddAutoMapper();
builder.AddOpenApiServices();
builder.AddApiVersioningConfig();
builder.AddControllers();
builder.AddSqlServerDatabase<AppDbContext>();
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

if (globalRateLimitEnabled)
{
    app.UseRateLimiting();
}

app.UseRequestMetrics();

var controllers = app.MapControllers();
if (globalRateLimitEnabled)
{
    controllers.RequireRateLimiting("GlobalRateLimit");
}

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.Run();
