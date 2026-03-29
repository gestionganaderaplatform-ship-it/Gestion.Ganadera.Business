using Gestion.Ganadera.API.Options;
using Gestion.Ganadera.API.Configuration.Providers;
using Gestion.Ganadera.Application.Abstractions.Interfaces;

namespace Gestion.Ganadera.API.Extensions;

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

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IApiInfoProvider, ApiInfoProvider>();
        builder.Services.AddSingleton<IExcelImportSettingsProvider, ExcelImportSettingsProvider>();
        builder.Services.AddScoped<ICurrentActorProvider, CurrentActorProvider>();
        return builder;
    }
}
