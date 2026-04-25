using System.Net;
using Microsoft.AspNetCore.HttpOverrides;

namespace Gestion.Ganadera.Business.API.Extensions
{
    /// <summary>
    /// Configura el procesamiento de cabeceras reenviadas cuando la API corre detras de proxy.
    /// </summary>
    public static class ForwardedHeadersExtensions
    {
        public static WebApplicationBuilder AddForwardedHeadersSupport(
            this WebApplicationBuilder builder)
        {
            var forwardedHeadersSection = builder.Configuration.GetSection("ForwardedHeaders");
            if (!forwardedHeadersSection.GetValue<bool>("Enabled"))
            {
                return builder;
            }

            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = forwardedHeadersSection.GetValue<int?>("ForwardLimit") ?? 1;
                options.RequireHeaderSymmetry =
                    forwardedHeadersSection.GetValue<bool?>("RequireHeaderSymmetry") ?? true;

                foreach (var proxyAddress in forwardedHeadersSection.GetSection("KnownProxies").Get<string[]>() ?? [])
                {
                    if (IPAddress.TryParse(proxyAddress, out var parsedAddress))
                    {
                        options.KnownProxies.Add(parsedAddress);
                    }
                }

                foreach (var network in forwardedHeadersSection.GetSection("KnownNetworks").GetChildren())
                {
                    var prefix = network.GetValue<string>("Prefix");
                    var prefixLength = network.GetValue<int?>("PrefixLength");
                    if (string.IsNullOrWhiteSpace(prefix) || prefixLength is null)
                    {
                        continue;
                    }

                    if (IPAddress.TryParse(prefix, out var parsedPrefix))
                    {
                        options.KnownNetworks.Add(
                            new Microsoft.AspNetCore.HttpOverrides.IPNetwork(parsedPrefix, prefixLength.Value));
                    }
                }
            });

            return builder;
        }
    }
}
