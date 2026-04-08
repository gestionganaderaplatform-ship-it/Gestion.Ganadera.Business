using Microsoft.Extensions.Options;
using Gestion.Ganadera.API.Options;
using Gestion.Ganadera.Application.Abstractions.Interfaces;

namespace Gestion.Ganadera.API.Configuration.Providers
{
    /// <summary>
    /// Expone la identidad tecnica del API a servicios que registran datos operativos.
    /// </summary>
    public sealed class ApiInfoProvider(IOptions<ApiInfoOptions> options) : IApiInfoProvider
    {
        private readonly ApiInfoOptions _options = options.Value;

        public string ApiCodigo =>
            ResolveApiCodigo(_options.Codigo);

        private static string ResolveApiCodigo(string fallback)
        {
            var explicitApiCodigo = Environment.GetEnvironmentVariable("ApiInfo__Codigo");
            if (!string.IsNullOrWhiteSpace(explicitApiCodigo))
            {
                return explicitApiCodigo;
            }

            var azureSiteName = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");
            if (!string.IsNullOrWhiteSpace(azureSiteName))
            {
                return azureSiteName;
            }

            return fallback;
        }
    }
}
