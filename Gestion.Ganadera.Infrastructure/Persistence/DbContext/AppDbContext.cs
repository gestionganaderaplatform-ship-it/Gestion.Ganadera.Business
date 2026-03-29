using Microsoft.EntityFrameworkCore;
using Gestion.Ganadera.Domain.Features.Seguridad;
using Gestion.Ganadera.Infrastructure.Observability.Models;
using Gestion.Ganadera.Infrastructure.Security.Models;

namespace Gestion.Ganadera.Infrastructure.Persistence
{
    /// <summary>
    /// DbContext principal del template. Centraliza el acceso EF Core y aplica las configuraciones del ensamblado.
    /// </summary>
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public DbSet<Auditoria> Auditorias { get; set; } = null!;
        public DbSet<MetricaSolicitud> Metrica_Solicitudes { get; set; } = null!;
        public DbSet<EventoSeguridad> Seguridad_Eventos => Set<EventoSeguridad>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
