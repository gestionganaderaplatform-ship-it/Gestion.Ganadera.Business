namespace Gestion.Ganadera.Business.API.Extensions
{
    /// <summary>
/// Registra la politica CORS base a partir de configuracion para clientes permitidos.
/// </summary>
public static class CorsExtensions
    {
        public static WebApplicationBuilder AddCorsServices(
            this WebApplicationBuilder builder)
        {
            var corsSection = builder.Configuration.GetSection("Cors");

            if (!corsSection.GetValue<bool>("Enabled"))
                return builder;

            var policyName = corsSection.GetValue<string>("PolicyName")!;

            var origins = corsSection
                .GetSection("AllowedOrigins")
                .Get<string[]>() ?? [];

            var methods = corsSection
                .GetSection("AllowedMethods")
                .Get<string[]>() ?? [];

            var headers = corsSection
                .GetSection("AllowedHeaders")
                .Get<string[]>() ?? [];

            var allowCredentials = corsSection.GetValue<bool>("AllowCredentials");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(policyName, policy =>
                {
                    policy
                        .WithOrigins(origins)
                        .WithMethods(methods)
                        .WithHeaders(headers);

                    if (allowCredentials)
                        policy.AllowCredentials();
                });
            });

            return builder;
        }
    }
}

