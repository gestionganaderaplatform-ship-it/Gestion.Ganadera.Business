using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Domain.Base;
using Gestion.Ganadera.Domain.Features.Seguridad;

namespace Gestion.Ganadera.Infrastructure.Persistence.Interceptors
{
    /// <summary>
    /// Completa datos de auditoria y registra cambios de entidades auditables durante SaveChanges.
    /// </summary>
    public class AuditSaveChangesInterceptor(
        IApiInfoProvider apiInfoProvider,
        ICurrentActorProvider currentActorProvider) : SaveChangesInterceptor
    {
        private readonly IApiInfoProvider _apiInfoProvider = apiInfoProvider;
        private readonly ICurrentActorProvider _currentActorProvider = currentActorProvider;

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            if (eventData.Context is not null)
            {
                AplicarAuditoria(eventData.Context);
            }

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                AplicarAuditoria(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AplicarAuditoria(DbContext context)
        {
            var entries = context.ChangeTracker.Entries<AuditableEntity>();
            var ahora = DateTime.Now;

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.Fecha_Creado == default)
                        {
                            entry.Entity.Fecha_Creado = ahora;
                        }

                        if (entry.Entity.Creado_Por == default &&
                            _currentActorProvider.ActorNumericId.HasValue)
                        {
                            entry.Entity.Creado_Por = _currentActorProvider.ActorNumericId.Value;
                        }
                        break;

                    case EntityState.Modified:
                        entry.Entity.Fecha_Modificado = ahora;

                        if (_currentActorProvider.ActorNumericId.HasValue)
                        {
                            entry.Entity.Modificado_Por = _currentActorProvider.ActorNumericId.Value;
                        }

                        RegistrarAuditoria(
                            context,
                            entry,
                            ahora,
                            _apiInfoProvider.ApiCodigo,
                            _currentActorProvider.ActorId);
                        break;
                }
            }
        }

        private static void RegistrarAuditoria(
            DbContext context,
            EntityEntry<AuditableEntity> entry,
            DateTime ahora,
            string apiCodigo,
            string? actorId)
        {
            var tableName = entry.Metadata.GetTableName() ?? "TablaDesconocida";
            var valoresAnteriores = entry.GetDatabaseValues();
            var valoresViejos = valoresAnteriores is null
                ? string.Empty
                : JsonConvert.SerializeObject(
                    entry.Properties
                        .Where(p => p.IsModified)
                        .ToDictionary(
                            p => p.Metadata.Name,
                            p => valoresAnteriores[p.Metadata.Name]));

            var auditoria = new Auditoria
            {
                Auditoria_Api_Codigo = apiCodigo,
                Auditoria_Nombre_Tabla = tableName,
                Auditoria_Valor_Clave = JsonConvert.SerializeObject(
                    entry.Properties
                        .Where(p => p.Metadata.IsPrimaryKey())
                        .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)),
                Auditoria_Valores_Viejos = valoresViejos,
                Auditoria_Nuevos_Valores = JsonConvert.SerializeObject(
                    entry.Properties
                        .Where(p => p.IsModified)
                        .ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)),
                Auditoria_Modificado_Por = actorId ?? string.Empty,
                Auditoria_Fecha_Modificado = ahora
            };

            context.Set<Auditoria>().Add(auditoria);
        }
    }
}

