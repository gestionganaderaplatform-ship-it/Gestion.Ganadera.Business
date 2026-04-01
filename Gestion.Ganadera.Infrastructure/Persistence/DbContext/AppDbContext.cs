using Microsoft.EntityFrameworkCore;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
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
        public DbSet<MetricaSolicitud> Metrica_Solicitudes { get; set; } = null!;
        public DbSet<EventoSeguridad> Seguridad_Eventos => Set<EventoSeguridad>();

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
            base.OnModelCreating(modelBuilder);
        }
    }
}
