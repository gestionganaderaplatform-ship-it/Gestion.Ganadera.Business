using Asp.Versioning;
using Gestion.Ganadera.Business.API.ErrorHandling;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.Procesos;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreProductivoMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/procesos/cambio-categoria")]
[ControllerPermissions(ControllerPermission.Create | ControllerPermission.GetPaged | ControllerPermission.Update)]
public class CambioCategoriaController(ICambioCategoriaService service) : ControllerBase
{
    [HttpGet("sugerencias")]
    [RequirePermission(ControllerPermission.GetPaged)]
    public async Task<IActionResult> ObtenerSugerencias(
        [FromQuery] long fincaCodigo,
        CancellationToken cancellationToken = default)
    {
        if (fincaCodigo <= 0)
        {
            return ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: CambioCategoriaMessages.FincaCodigoInvalido);
        }

        var sugerencias = await service.ObtenerSugerenciasPendientesAsync(fincaCodigo, cancellationToken);

        return Ok(sugerencias);
    }

    [HttpPost("generar-sugerencias")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> GenerarSugerencias(
        [FromQuery] long fincaCodigo,
        CancellationToken cancellationToken = default)
    {
        if (fincaCodigo <= 0)
        {
            return ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: CambioCategoriaMessages.FincaCodigoInvalido);
        }

        var cantidad = await service.GenerarSugerenciasAsync(fincaCodigo, cancellationToken);

        return Ok(new { Cantidad_Generada = cantidad });
    }

    [HttpPost("aprobar-lote")]
    [RequirePermission(ControllerPermission.Update)]
    public async Task<IActionResult> AprobarLote(
        [FromBody] IEnumerable<long> sugerenciasCodigos,
        CancellationToken cancellationToken = default)
    {
        if (sugerenciasCodigos == null || !sugerenciasCodigos.Any())
        {
            return ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: CambioCategoriaMessages.SugerenciasNoEncontradas);
        }

        var exito = await service.AprobarSugerenciasAsync(sugerenciasCodigos, cancellationToken);

        return exito
            ? Ok(new { Mensaje = CambioCategoriaMessages.SugerenciaProcesadaExitosamente })
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: CambioCategoriaMessages.ErrorAlProcesarAprobacion);
    }

    [HttpPost("rechazar-lote")]
    [RequirePermission(ControllerPermission.Update)]
    public async Task<IActionResult> RechazarLote(
        [FromBody] IEnumerable<long> sugerenciasCodigos,
        CancellationToken cancellationToken = default)
    {
        if (sugerenciasCodigos == null || !sugerenciasCodigos.Any())
        {
            return ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: CambioCategoriaMessages.SugerenciasNoEncontradas);
        }

        var exito = await service.RechazarSugerenciasAsync(sugerenciasCodigos, cancellationToken);

        return exito
            ? Ok(new { Mensaje = CambioCategoriaMessages.SugerenciaProcesadaExitosamente })
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: CambioCategoriaMessages.ErrorAlProcesarRechazo);
    }
}
