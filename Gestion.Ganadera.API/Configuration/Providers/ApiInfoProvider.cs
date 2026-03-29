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

        public string ApiCodigo => _options.Codigo;
    }
}
