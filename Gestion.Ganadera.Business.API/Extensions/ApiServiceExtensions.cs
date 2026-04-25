using Gestion.Ganadera.Business.API.Configuration.Providers;
using Gestion.Ganadera.Business.API.Options;
using Gestion.Ganadera.Business.API.Security.Sesiones;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Extensions.Http;
using System.Net;
using System.Net.Http.Headers;

namespace Gestion.Ganadera.Business.API.Extensions;

/// <summary>
/// Registra servicios transversales del host antes de componer el resto del API.
/// </summary>
public static class ApiServiceExtensions
{
    public static WebApplicationBuilder AddApiServices(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiInfoOptions>(
            builder.Configuration.GetSection(ApiInfoOptions.SectionName));
        builder.Services.Configure<ExcelImportOptions>(
            builder.Configuration.GetSection(ExcelImportOptions.SectionName));
        builder.Services.Configure<OpcionesApiAuth>(
            builder.Configuration.GetSection(OpcionesApiAuth.SectionName));

builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IApiInfoProvider, ApiInfoProvider>();
        builder.Services.AddSingleton<IExcelImportSettingsProvider, ExcelImportSettingsProvider>();

        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    if (outcome.Exception != null)
                    {
                        Serilog.Log.Warning(
                            outcome.Exception,
                            "Retry {RetryAttempt} after {TotalSeconds}s due to: {Error}",
                            retryAttempt,
                            timespan.TotalSeconds,
                            outcome.Exception.Message);
                    }
                    else if (outcome.Result != null)
                    {
                        Serilog.Log.Warning(
                            "Retry {RetryAttempt} after {TotalSeconds}s due to status code: {StatusCode}",
                            retryAttempt,
                            timespan.TotalSeconds,
                            outcome.Result.StatusCode);
                    }
                });

        builder.Services.AddHttpClient<IServicioValidacionSesionRemota, ServicioValidacionSesionRemota>((serviceProvider, client) =>
        {
            var opciones = serviceProvider
                .GetRequiredService<Microsoft.Extensions.Options.IOptions<OpcionesApiAuth>>()
                .Value;

            if (Uri.TryCreate(opciones.BaseUrl, UriKind.Absolute, out var baseUri))
            {
                client.BaseAddress = baseUri;
            }

            client.Timeout = TimeSpan.FromSeconds(
                opciones.TimeoutSeconds > 0 ? opciones.TimeoutSeconds : 5);
        })
        .AddPolicyHandler(retryPolicy);

        builder.Services.AddScoped<ICurrentActorProvider, CurrentActorProvider>();
        builder.Services.AddScoped<ICurrentClientProvider, CurrentClientProvider>();
        return builder;
    }
}
