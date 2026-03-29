using Gestion.Ganadera.API.Conventions;
using Gestion.Ganadera.API.Filters;
namespace Gestion.Ganadera.API.Extensions
{
    /// <summary>
    /// Configura MVC, serializacion JSON y filtros globales usados por los controllers.
    /// </summary>
    public static class ControllersExtensions
    {
        public static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new PermissionApplicationModelConvention());
                options.MaxModelBindingCollectionSize = 1000;
                options.Filters.Add<ValidateModelStateFilter>();
            });
            return builder;
        }
    }
}

