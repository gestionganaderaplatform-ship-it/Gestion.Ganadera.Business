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
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Interfaces;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;
using Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Interfaces;

using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Interfaces;

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
            builder.Services.AddScoped<IIdentificadorService, IdentificadorService>();
            builder.Services.AddScoped<IAnimalConsultaRepository, AnimalConsultaRepository>();

            builder.Services.AddScoped<IAnimalConsultaService, AnimalConsultaService>();
            builder.Services.AddScoped<IValidarRegistroExistenteRepository, ValidarRegistroExistenteRepository>();
            builder.Services.AddScoped<IRegistroExistenteRepository, RegistroExistenteRepository>();
            builder.Services.AddScoped<IRegistroExistenteService, RegistroExistenteService>();
            builder.Services.AddScoped<ICompraRepository, CompraRepository>();
            builder.Services.AddScoped<ICompraService, CompraService>();
            builder.Services.AddScoped<INacimientoRepository, NacimientoRepository>();
            builder.Services.AddScoped<INacimientoService, NacimientoService>();
            builder.Services.AddScoped<IPesajeRepository, PesajeRepository>();
            builder.Services.AddScoped<IPesajeService, PesajeService>();
            builder.Services.AddScoped<IMuerteRepository, MuerteRepository>();
            builder.Services.AddScoped<IMuerteService, MuerteService>();
            builder.Services.AddScoped<IVentaRepository, VentaRepository>();
            builder.Services.AddScoped<IVentaService, VentaService>();
            builder.Services.AddScoped<ICausaMuerteRepository, CausaMuerteRepository>();
            builder.Services.AddScoped<ICausaMuerteService, CausaMuerteService>();
            builder.Services.AddScoped<IMovimientoPotreroRepository, MovimientoPotreroRepository>();
            builder.Services.AddScoped<IMovimientoPotreroService, MovimientoPotreroService>();
            builder.Services.AddScoped<IValidarVacunacionRepository, VacunacionRepository>();
            builder.Services.AddScoped<IVacunacionRepository, VacunacionRepository>();
            builder.Services.AddScoped<IVacunacionService, VacunacionService>();
            builder.Services.AddScoped<IVacunaRepository, VacunaRepository>();
            builder.Services.AddScoped<IVacunaService, VacunaService>();
            builder.Services.AddScoped<ITrasladoFincaRepository, TrasladoFincaRepository>();
            builder.Services.AddScoped<ITrasladoFincaService, TrasladoFincaService>();
            builder.Services.AddScoped<ITratamientoTipoRepository, TratamientoTipoRepository>();
            builder.Services.AddScoped<ITratamientoTipoService, TratamientoTipoService>();
            builder.Services.AddScoped<ITratamientoProductoRepository, TratamientoProductoRepository>();
            builder.Services.AddScoped<ITratamientoProductoService, TratamientoProductoService>();
            builder.Services.AddScoped<ITratamientoSanitarioRepository, TratamientoSanitarioRepository>();
            builder.Services.AddScoped<ITratamientoSanitarioService, TratamientoSanitarioService>();
            builder.Services.AddScoped<IPalpacionResultadoRepository, PalpacionResultadoRepository>();
            builder.Services.AddScoped<IPalpacionResultadoService, PalpacionResultadoService>();
            builder.Services.AddScoped<IPalpacionRepository, PalpacionRepository>();
            builder.Services.AddScoped<IPalpacionService, PalpacionService>();
            builder.Services.AddScoped<IDesteteRepository, DesteteRepository>();
            builder.Services.AddScoped<IDesteteService, DesteteService>();
            builder.Services.AddScoped<IDescarteMotivoRepository, DescarteMotivoRepository>();
            builder.Services.AddScoped<IDescarteMotivoService, DescarteMotivoService>();
            builder.Services.AddScoped<IDescarteRepository, DescarteRepository>();
            builder.Services.AddScoped<IDescarteService, DescarteService>();
            builder.Services.AddScoped<ICambioCategoriaRepository, CambioCategoriaRepository>();
            builder.Services.AddScoped<ICambioCategoriaService, CambioCategoriaService>();

            return builder;
        }
    }
}
