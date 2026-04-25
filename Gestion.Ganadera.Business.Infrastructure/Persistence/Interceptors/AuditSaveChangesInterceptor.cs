using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Domain.Base;
using Gestion.Ganadera.Business.Domain.Features.Seguridad;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Interceptors
{
    /// <summary>
    /// Completa datos de auditoria y registra cambios de entidades auditables durante SaveChanges.
    /// </summary>
    public class AuditSaveChangesInterceptor(
        IApiInfoProvider apiInfoProvider,
        ICurrentActorProvider currentActorProvider,
        ICurrentClientProvider currentClientProvider) : SaveChangesInterceptor
    {
        private readonly IApiInfoProvider _apiInfoProvider = apiInfoProvider;
        private readonly ICurrentActorProvider _currentActorProvider = currentActorProvider;
        private readonly ICurrentClientProvider _currentClientProvider = currentClientProvider;

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
                        if (!entry.Entity.Cliente_Codigo.HasValue &&
                            _currentClientProvider.ClientNumericId.HasValue)
                        {
                            entry.Entity.Cliente_Codigo = _currentClientProvider.ClientNumericId.Value;
                        }

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
                            _currentActorProvider.ActorEmail ?? _currentActorProvider.ActorId,
                            _currentClientProvider.ClientNumericId);
                        break;
                }
            }
        }

        private static void RegistrarAuditoria(
            DbContext context,
            EntityEntry<AuditableEntity> entry,
            DateTime ahora,
            string apiCodigo,
            string? actorId,
            long? clienteCodigo)
        {
            var tableName = ResolveTableName(entry);
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
                Cliente_Codigo = clienteCodigo,
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

        private static string ResolveTableName(EntityEntry<AuditableEntity> entry)
        {
            var tableName = entry.Metadata.GetTableName();
            if (string.IsNullOrWhiteSpace(tableName))
            {
                return "TablaDesconocida";
            }

            var schema = entry.Metadata.GetSchema();
            return string.IsNullOrWhiteSpace(schema)
                ? tableName
                : $"{schema}.{tableName}";
        }
    }
}

