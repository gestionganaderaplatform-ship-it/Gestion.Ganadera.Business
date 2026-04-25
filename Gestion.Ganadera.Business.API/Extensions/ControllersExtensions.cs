using Gestion.Ganadera.Business.API.Conventions;
using Gestion.Ganadera.Business.API.Filters;
namespace Gestion.Ganadera.Business.API.Extensions
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

