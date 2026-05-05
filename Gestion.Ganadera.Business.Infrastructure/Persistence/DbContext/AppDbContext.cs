using Microsoft.EntityFrameworkCore;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Domain.Features.Navegacion;
using Gestion.Ganadera.Business.Domain.Features.Seguridad;
using Gestion.Ganadera.Business.Infrastructure.Observability.Models;
using Gestion.Ganadera.Business.Infrastructure.Security.Models;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence
{
    /// <summary>
    /// DbContext principal del template. Centraliza el acceso EF Core y aplica las configuraciones del ensamblado.
    /// </summary>
    public class AppDbContext(
        DbContextOptions<AppDbContext> options,
        ICurrentClientProvider? currentClientProvider = null)
        : DbContext(options)
    {
        private readonly ICurrentClientProvider? _currentClientProvider = currentClientProvider;

        public DbSet<Auditoria> Auditorias { get; set; } = null!;
        public DbSet<Animal> Animales { get; set; } = null!;
        public DbSet<AnimalRelacionFamiliar> AnimalesRelacionesFamiliares { get; set; } = null!;
        public DbSet<CategoriaAnimal> CategoriasAnimal { get; set; } = null!;
        public DbSet<EventoDetalleRegistroExistente> EventosDetalleRegistroExistente { get; set; } = null!;
        public DbSet<EventoDetalleCompra> EventosDetalleCompra { get; set; } = null!;
public DbSet<EventoDetalleNacimiento> EventosDetalleNacimiento { get; set; } = null!;
        public DbSet<EventoDetallePesaje> EventosDetallePesaje { get; set; } = null!;
        public DbSet<EventoDetalleMuerte> EventosDetalleMuerte { get; set; } = null!;
        public DbSet<EventoDetalleVenta> EventosDetalleVenta { get; set; } = null!;
        public DbSet<EventoDetalleMovimientoPotrero> EventosDetalleMovimientoPotrero { get; set; } = null!;
        public DbSet<EventoGanadero> EventosGanaderos { get; set; } = null!;
        public DbSet<EventoGanaderoAnimal> EventosGanaderosAnimal { get; set; } = null!;
        public DbSet<Finca> Fincas { get; set; } = null!;
        public DbSet<IdentificadorAnimal> IdentificadoresAnimal { get; set; } = null!;
        public DbSet<MenuNavegacion> MenusNavegacion { get; set; } = null!;
        public DbSet<LogAplicacion> Logs { get; set; } = null!;
        public DbSet<MetricaSolicitud> MetricasSolicitud { get; set; } = null!;
        public DbSet<Potrero> Potreros { get; set; } = null!;
        public DbSet<RangoEdad> RangosEdad { get; set; } = null!;
        public DbSet<EventoSeguridad> Seguridad_Eventos => Set<EventoSeguridad>();
        public DbSet<TipoIdentificador> TiposIdentificador { get; set; } = null!;
        public DbSet<CausaMuerte> CausasMuerte { get; set; } = null!;
        public DbSet<VacunaEnfermedad> VacunasEnfermedades { get; set; } = null!;
        public DbSet<Vacuna> Vacunas { get; set; } = null!;
        public DbSet<EventoDetalleVacunacion> EventosDetalleVacunacion { get; set; } = null!;
        public DbSet<EventoDetalleTrasladoFinca> EventosDetalleTrasladoFinca { get; set; } = null!;
        public DbSet<TratamientoTipo> TratamientosTipos { get; set; } = null!;
        public DbSet<TratamientoProducto> TratamientosProductos { get; set; } = null!;
        public DbSet<EventoDetalleTratamientoSanitario> EventosDetalleTratamientoSanitario { get; set; } = null!;
        public DbSet<PalpacionResultado> PalpacionesResultados { get; set; } = null!;
        public DbSet<EventoDetallePalpacion> EventosDetallePalpacion { get; set; } = null!;
        public DbSet<EventoDetalleDestete> EventosDetalleDestete { get; set; } = null!;
        public DbSet<DescarteMotivo> DescartesMotivos { get; set; } = null!;
        public DbSet<EventoDetalleDescarte> EventosDetalleDescarte { get; set; } = null!;
        public DbSet<CambioCategoriaSugerido> CambiosCategoriaSugeridos { get; set; } = null!;
        public DbSet<EventoDetalleCambioCategoria> EventosDetalleCambioCategoria { get; set; } = null!;


        private long? CurrentClientNumericId => _currentClientProvider?.ClientNumericId;
        private bool IsClientScopeDisabled => _currentClientProvider is null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            modelBuilder.Entity<Auditoria>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<MetricaSolicitud>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoSeguridad>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<LogAplicacion>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<Finca>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<CategoriaAnimal>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<RangoEdad>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<TipoIdentificador>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<Animal>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<AnimalRelacionFamiliar>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<Potrero>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<IdentificadorAnimal>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoGanadero>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoGanaderoAnimal>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleRegistroExistente>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleCompra>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
modelBuilder.Entity<EventoDetalleNacimiento>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetallePesaje>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleMuerte>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleVenta>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleMovimientoPotrero>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
modelBuilder.Entity<CausaMuerte>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<VacunaEnfermedad>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<Vacuna>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleVacunacion>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleTrasladoFinca>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<TratamientoTipo>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<TratamientoProducto>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleTratamientoSanitario>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<PalpacionResultado>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetallePalpacion>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleDestete>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<DescarteMotivo>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleDescarte>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<CambioCategoriaSugerido>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            modelBuilder.Entity<EventoDetalleCambioCategoria>()
                .HasQueryFilter(entity => IsClientScopeDisabled || entity.Cliente_Codigo == CurrentClientNumericId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
