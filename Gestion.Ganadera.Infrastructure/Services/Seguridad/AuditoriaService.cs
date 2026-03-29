using AutoMapper;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Interfaces;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.ViewModels;
using Gestion.Ganadera.Domain.Features.Seguridad;
using Gestion.Ganadera.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Infrastructure.Services.Seguridad
{
    /// <summary>
    /// Servicio de aplicacion para consultar y exponer registros de auditoria.
    /// </summary>
    public class AuditoriaService(IAuditoriaRepository repository, IMapper mapper)
        : BaseService<Auditoria, AuditoriaViewModel, AuditoriaCreateViewModel, AuditoriaUpdateViewModel, IAuditoriaRepository, AuditoriaExportFilterViewModel>(repository, mapper), IAuditoriaService
    {
        protected override async Task<List<Auditoria>> ObtenerEntidadesParaExportarAsync(AuditoriaExportFilterViewModel filtro)
        {
            var filtros = new Dictionary<string, object>
            {
                [nameof(Auditoria.Auditoria_Fecha_Modificado)] = filtro.Auditoria_Fecha_Modificado_Desde!.Value
            };

            if (!string.IsNullOrWhiteSpace(filtro.Auditoria_Nombre_Tabla))
            {
                filtros[nameof(Auditoria.Auditoria_Nombre_Tabla)] = filtro.Auditoria_Nombre_Tabla.Trim();
            }

            if (!string.IsNullOrWhiteSpace(filtro.Auditoria_Modificado_Por))
            {
                filtros[nameof(Auditoria.Auditoria_Modificado_Por)] = filtro.Auditoria_Modificado_Por.Trim();
            }

            var entidades = await _repository.FiltrarPorPropiedades(filtros);

            return entidades
                .Where(x => x.Auditoria_Fecha_Modificado <= filtro.Auditoria_Fecha_Modificado_Hasta!.Value)
                .OrderByDescending(x => x.Auditoria_Fecha_Modificado)
                .ToList();
        }
    }
}
