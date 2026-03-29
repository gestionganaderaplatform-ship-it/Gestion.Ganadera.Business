using Asp.Versioning;

namespace Gestion.Ganadera.API.Extensions
{
    /// <summary>
    /// Configura versionado del API y su integracion con exploracion de endpoints.
    /// </summary>
    public static class ApiVersioningExtensions
    {
        public static WebApplicationBuilder AddApiVersioningConfig(
            this WebApplicationBuilder builder)
        {
            builder.Services
                .AddApiVersioning(options =>
                {
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                })
                .AddMvc()
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return builder;
        }
    }
}
