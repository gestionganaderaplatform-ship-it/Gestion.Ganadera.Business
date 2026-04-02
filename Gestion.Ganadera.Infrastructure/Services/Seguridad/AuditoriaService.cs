using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Interfaces;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.ViewModels;
using Gestion.Ganadera.Domain.Features.Seguridad;
using Gestion.Ganadera.Infrastructure.Persistence;
using Gestion.Ganadera.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Infrastructure.Services.Seguridad
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

            var entidades = await query.ToListAsync();

            return entidades
                .Where(x => x.Auditoria_Fecha_Modificado <= filtro.Auditoria_Fecha_Modificado_Hasta!.Value)
                .OrderByDescending(x => x.Auditoria_Fecha_Modificado)
                .ToList();
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

            return query;
        }

        private IQueryable<Auditoria> BuildScopedQuery(long clientCode)
        {
            return _dbContext.Auditorias
                .AsNoTracking()
                .Where(item => item.Cliente_Codigo == clientCode);
        }
    }
}
