using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Gestion.Ganadera.API.Controllers.Base;
using Gestion.Ganadera.API.ErrorHandling;
using Gestion.Ganadera.API.Security.Permissions;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Interfaces;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.ViewModels;

namespace Gestion.Ganadera.API.Controllers.Seguridad.Auditoria
{
    /// <summary>
    /// Expone consulta y administracion basica de los registros de auditoria del sistema.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/seguridad/auditoria")]
    [ControllerPermissions(
       ControllerPermission.GetPaged
       | ControllerPermission.GetAll
       | ControllerPermission.GetById
       | ControllerPermission.Filter
       | ControllerPermission.ExportExcel
   )]
    public class AuditoriaController(
        IAuditoriaService service,
        IValidator<AuditoriaExportFilterViewModel> exportValidator,
        ILogger<AuditoriaController> logger)
        : BaseController<AuditoriaViewModel, AuditoriaCreateViewModel, AuditoriaUpdateViewModel, AuditoriaExportFilterViewModel>(
            service,
            logger,
            exportValidator)
    {
        [HttpPost("consultar-paginado")]
        [RequirePermission(ControllerPermission.Filter)]
        public async Task<IActionResult> ConsultarPaginado(
            [FromBody] AuditoriaViewModel filtro,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            if (filtro.Auditoria_Fecha_Modificado_Desde.HasValue &&
                filtro.Auditoria_Fecha_Modificado_Hasta.HasValue &&
                filtro.Auditoria_Fecha_Modificado_Hasta.Value < filtro.Auditoria_Fecha_Modificado_Desde.Value)
            {
                return ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: "La fecha fin no puede ser menor que la fecha inicio.");
            }

            var filtros = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(filtro.Auditoria_Nombre_Tabla))
            {
                filtros[nameof(AuditoriaViewModel.Auditoria_Nombre_Tabla)] = filtro.Auditoria_Nombre_Tabla.Trim();
            }

            if (!string.IsNullOrWhiteSpace(filtro.Auditoria_Modificado_Por))
            {
                filtros[nameof(AuditoriaViewModel.Auditoria_Modificado_Por)] = filtro.Auditoria_Modificado_Por.Trim();
            }

            if (filtro.Auditoria_Fecha_Modificado_Desde.HasValue)
            {
                filtros[nameof(AuditoriaViewModel.Auditoria_Fecha_Modificado_Desde)] =
                    filtro.Auditoria_Fecha_Modificado_Desde.Value;
            }

            if (filtro.Auditoria_Fecha_Modificado_Hasta.HasValue)
            {
                filtros[nameof(AuditoriaViewModel.Auditoria_Fecha_Modificado_Hasta)] =
                    filtro.Auditoria_Fecha_Modificado_Hasta.Value;
            }

            var pageNumberNormalizado = pageNumber <= 0 ? 1 : pageNumber;
            var pageSizeNormalizado = pageSize <= 0 ? 25 : Math.Min(pageSize, 100);

            if (filtros.Count == 0)
            {
                var (itemsSinFiltro, totalSinFiltro) = await service.ObtenerPorPaginado(pageNumberNormalizado, pageSizeNormalizado);
                return Ok(new { Items = itemsSinFiltro, TotalRegistros = totalSinFiltro });
            }

            var (items, totalRegistros) = await service.FiltrarPorPropiedadesPaginadoAsync(
                filtros,
                pageNumberNormalizado,
                pageSizeNormalizado);

            return Ok(new { Items = items, TotalRegistros = totalRegistros });
        }
    }
}
