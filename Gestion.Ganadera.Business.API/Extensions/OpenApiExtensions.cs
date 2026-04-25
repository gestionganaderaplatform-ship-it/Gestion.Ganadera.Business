using Microsoft.OpenApi;
using Gestion.Ganadera.Business.API.OpenApi;

namespace Gestion.Ganadera.Business.API.Extensions;

/// <summary>
/// Registra OpenAPI y las personalizaciones necesarias para documentar el template.
/// </summary>
public static class OpenApiExtensions
{
    public static WebApplicationBuilder AddOpenApiServices(this WebApplicationBuilder builder)
    {
        var jwtEnabled = builder.Configuration.GetValue<bool>("Jwt:Enabled");

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Gestion.Ganadera.Business.API",
                Version = "v1"
            });

            options.OperationFilter<BaseControllerRequestBodyOperationFilter>();

            if (jwtEnabled)
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Escriba el encabezado completo: Bearer {token}"
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [
                        new OpenApiSecuritySchemeReference("Bearer", document, null)
                    ] = []
                });
            }
        });

        return builder;
    }
}
