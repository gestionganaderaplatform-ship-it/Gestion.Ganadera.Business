using Microsoft.EntityFrameworkCore;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Domain.Features.Navegacion;
using Gestion.Ganadera.Domain.Features.Seguridad;
using Gestion.Ganadera.Infrastructure.Observability.Models;
using Gestion.Ganadera.Infrastructure.Security.Models;

namespace Gestion.Ganadera.Infrastructure.Persistence
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
        public DbSet<CategoriaAnimal> CategoriasAnimal { get; set; } = null!;
        public DbSet<EventoDetalleRegistroExistente> EventosDetalleRegistroExistente { get; set; } = null!;
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
