using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Seguridad.Auditoria.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Seguridad.Auditoria.Messages;
using Gestion.Ganadera.Business.Application.Features.Seguridad.Auditoria.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Seguridad;
using Gestion.Ganadera.Business.Infrastructure.Persistence;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Seguridad
{
    /// <summary>
    /// Servicio de aplicacion para consultar y exponer registros de auditoria.
    /// </summary>
    public class AuditoriaService(
        IAuditoriaRepository repository,
        IMapper mapper,
        AppDbContext dbContext,
        ICurrentClientProvider currentClientProvider)
        : BaseService<Auditoria, AuditoriaViewModel, AuditoriaCreateViewModel, AuditoriaUpdateViewModel, IAuditoriaRepository, AuditoriaExportFilterViewModel>(repository, mapper), IAuditoriaService
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly ICurrentClientProvider _currentClientProvider = currentClientProvider;

        public new async Task<(IEnumerable<AuditoriaViewModel> Items, int TotalRegistros)> ObtenerPorPaginado(int pagina, int tamañoPagina)
        {
            var clientCode = _currentClientProvider.ClientNumericId;
            if (!clientCode.HasValue)
            {
                return (Enumerable.Empty<AuditoriaViewModel>(), 0);
            }

            var pageNumber = pagina <= 0 ? 1 : pagina;
            var pageSize = tamañoPagina <= 0 ? 25 : Math.Min(tamañoPagina, 100);

            var query = BuildScopedQuery(clientCode.Value);
            var totalRegistros = await query.CountAsync();
            var entities = await query
                .OrderByDescending(item => item.Auditoria_Fecha_Modificado)
                .ThenByDescending(item => item.Auditoria_Codigo)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (_mapper.Map<IEnumerable<AuditoriaViewModel>>(entities), totalRegistros);
        }

        public new async Task<IEnumerable<AuditoriaViewModel>> FiltrarPorPropiedadesAsync(Dictionary<string, object> filtros)
        {
            var entities = await BuildFilteredScopedQuery(filtros).ToListAsync();
            return _mapper.Map<IEnumerable<AuditoriaViewModel>>(entities);
        }

        public new async Task<(IEnumerable<AuditoriaViewModel> Items, int TotalRegistros)> FiltrarPorPropiedadesPaginadoAsync(
            Dictionary<string, object> filtros,
            int pageNumber,
            int pageSize)
        {
            var safePageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var safePageSize = pageSize <= 0 ? 25 : Math.Min(pageSize, 100);
            var query = BuildFilteredScopedQuery(filtros);
            var totalRegistros = await query.CountAsync();
            var entities = await query
                .OrderByDescending(item => item.Auditoria_Fecha_Modificado)
                .ThenByDescending(item => item.Auditoria_Codigo)
                .Skip((safePageNumber - 1) * safePageSize)
                .Take(safePageSize)
                .ToListAsync();

            return (_mapper.Map<IEnumerable<AuditoriaViewModel>>(entities), totalRegistros);
        }

        protected override async Task<List<Auditoria>> ObtenerEntidadesParaExportarAsync(AuditoriaExportFilterViewModel filtro)
        {
            var clientCode = _currentClientProvider.ClientNumericId;
            if (!clientCode.HasValue)
            {
                return [];
            }

            var query = BuildScopedQuery(clientCode.Value)
                .Where(item => item.Auditoria_Fecha_Modificado >= filtro.Auditoria_Fecha_Modificado_Desde!.Value);

            if (!string.IsNullOrWhiteSpace(filtro.Auditoria_Nombre_Tabla))
            {
                var tableName = filtro.Auditoria_Nombre_Tabla.Trim();
                query = query.Where(item => item.Auditoria_Nombre_Tabla.Contains(tableName));
            }

            if (!string.IsNullOrWhiteSpace(filtro.Auditoria_Modificado_Por))
            {
                var actorId = filtro.Auditoria_Modificado_Por.Trim();
                query = query.Where(item => item.Auditoria_Modificado_Por.Contains(actorId));
            }

            if (!string.IsNullOrWhiteSpace(filtro.Auditoria_Valor_Clave))
            {
                var keyValue = filtro.Auditoria_Valor_Clave.Trim();
                query = query.Where(item => item.Auditoria_Valor_Clave.Contains(keyValue));
            }

            var entidades = await query.ToListAsync();

            return entidades
                .Where(x => x.Auditoria_Fecha_Modificado <= filtro.Auditoria_Fecha_Modificado_Hasta!.Value)
                .OrderByDescending(x => x.Auditoria_Fecha_Modificado)
                .ToList();
        }

        protected override void ValidarExportacion(IReadOnlyCollection<Auditoria> entidades)
        {
            if (entidades.Count > 0)
            {
                return;
            }

            throw new ValidationException([
                new ValidationFailure(
                    nameof(AuditoriaExportFilterViewModel.Auditoria_Fecha_Modificado_Desde),
                    AuditoriaValidationMessages.ExportNoDataFound)
            ]);
        }

        private IQueryable<Auditoria> BuildFilteredScopedQuery(Dictionary<string, object> filtros)
        {
            var clientCode = _currentClientProvider.ClientNumericId;
            if (!clientCode.HasValue)
            {
                return Enumerable.Empty<Auditoria>().AsQueryable();
            }

            var query = BuildScopedQuery(clientCode.Value);

            if (filtros.TryGetValue(nameof(Auditoria.Auditoria_Nombre_Tabla), out var tableNameValue) &&
                tableNameValue is string tableName &&
                !string.IsNullOrWhiteSpace(tableName))
            {
                var normalizedTableName = tableName.Trim();
                query = query.Where(item => item.Auditoria_Nombre_Tabla.Contains(normalizedTableName));
            }

            if (filtros.TryGetValue(nameof(Auditoria.Auditoria_Modificado_Por), out var modifiedByValue) &&
                modifiedByValue is string modifiedBy &&
                !string.IsNullOrWhiteSpace(modifiedBy))
            {
                var normalizedModifiedBy = modifiedBy.Trim();
                query = query.Where(item => item.Auditoria_Modificado_Por.Contains(normalizedModifiedBy));
            }

            if (filtros.TryGetValue(nameof(Auditoria.Auditoria_Valor_Clave), out var keyValueObject) &&
                keyValueObject is string keyValue &&
                !string.IsNullOrWhiteSpace(keyValue))
            {
                var normalizedKeyValue = keyValue.Trim();
                query = query.Where(item => item.Auditoria_Valor_Clave.Contains(normalizedKeyValue));
            }

            if (filtros.TryGetValue(nameof(AuditoriaViewModel.Auditoria_Fecha_Modificado_Desde), out var modifiedFromValue) &&
                TryResolveDate(modifiedFromValue, out var modifiedFrom))
            {
                query = query.Where(item => item.Auditoria_Fecha_Modificado >= modifiedFrom);
            }

            if (filtros.TryGetValue(nameof(AuditoriaViewModel.Auditoria_Fecha_Modificado_Hasta), out var modifiedToValue) &&
                TryResolveDate(modifiedToValue, out var modifiedTo))
            {
                query = query.Where(item => item.Auditoria_Fecha_Modificado <= modifiedTo);
            }

            return query;
        }

        private IQueryable<Auditoria> BuildScopedQuery(long clientCode)
        {
            return _dbContext.Auditorias
                .AsNoTracking()
                .Where(item => item.Cliente_Codigo == clientCode);
        }

        private static bool TryResolveDate(object? value, out DateTime resolved)
        {
            switch (value)
            {
                case DateTime dateTime:
                    resolved = dateTime;
                    return true;
                case string rawValue when DateTime.TryParse(rawValue, out var parsed):
                    resolved = parsed;
                    return true;
                default:
                    resolved = default;
                    return false;
            }
        }
    }
}
