using FluentValidation;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Navegacion.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Interfaces;
using Gestion.Ganadera.Business.Application.Observability.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Base.Validators;
using Gestion.Ganadera.Business.Infrastructure.Persistence;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Metadata;
using Gestion.Ganadera.Business.Infrastructure.Seguridad;
using Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Navegacion;
using Gestion.Ganadera.Business.Infrastructure.Services.Observability;
using Gestion.Ganadera.Business.Infrastructure.Services.Seguridad;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.Interfaces;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Interfaces;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;
using Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

namespace Gestion.Ganadera.Business.API.Extensions
{
    /// <summary>
    /// Conecta contratos de Application con implementaciones del proyecto y de infraestructura.
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        public static WebApplicationBuilder AddProjectDependencyInjection(
            this WebApplicationBuilder builder)
        {
            var repositoryAssembly = typeof(IBaseRepository<>).Assembly;
            var serviceAssembly = typeof(IBaseService<,,>).Assembly;
            var validatorsAssembly = typeof(CodigoRequestValidator).Assembly;
            var persistenceAssembly = typeof(AppDbContext).Assembly;

            builder.Services.Scan(scan => scan
                .FromAssemblies(repositoryAssembly, persistenceAssembly, serviceAssembly, validatorsAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IBaseRepository<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IBaseService<,,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );

            builder.Services.AddScoped<IEntityValidationMetadata, EfEntityValidationMetadata>();
            builder.Services.AddScoped<IEntitySchemaMetadata, EfEntitySchemaMetadata>();
            builder.Services.AddScoped<IGanaderiaCatalogBootstrapService, GanaderiaCatalogBootstrapService>();
            builder.Services.AddScoped<IMenuNavegacionService, MenuNavegacionService>();
            builder.Services.AddScoped<IRequestMetricasService, RequestMetricasService>();
            builder.Services.AddScoped<ISecurityEventService, SecurityEventService>();
            builder.Services.AddScoped<IAnimalConsultaRepository, AnimalConsultaRepository>();
            builder.Services.AddScoped<IAnimalConsultaService, AnimalConsultaService>();
            builder.Services.AddScoped<IValidarRegistroExistenteRepository, ValidarRegistroExistenteRepository>();
            builder.Services.AddScoped<IRegistroExistenteRepository, RegistroExistenteRepository>();
            builder.Services.AddScoped<IRegistroExistenteService, RegistroExistenteService>();
            builder.Services.AddScoped<ICompraRepository, CompraRepository>();
            builder.Services.AddScoped<ICompraService, CompraService>();
            builder.Services.AddScoped<INacimientoRepository, NacimientoRepository>();
            builder.Services.AddScoped<INacimientoService, NacimientoService>();

            return builder;
        }
    }
}
