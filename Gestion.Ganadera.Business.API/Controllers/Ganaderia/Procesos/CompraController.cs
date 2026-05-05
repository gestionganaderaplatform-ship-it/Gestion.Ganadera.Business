using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.Business.API.ErrorHandling;
using Gestion.Ganadera.Business.API.Requests.Helpers;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.Procesos;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreProductivoMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/procesos/compra")]
[ControllerPermissions(ControllerPermission.Create | ControllerPermission.GetPaged)]
public class CompraController(
    ICompraService service,
    IIdentificadorService identificadorService) : ControllerBase
{
    [HttpGet("siguiente-consecutivo")]
    [RequirePermission(ControllerPermission.GetPaged)]
    public async Task<IActionResult> ObtenerSiguienteConsecutivo(
        [FromQuery] long fincaCodigo,
        CancellationToken cancellationToken = default)
    {
        if (fincaCodigo <= 0)
        {
            return ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: CompraMessages.FincaCodigoInvalido);
        }

        var consecutivo = await identificadorService.ObtenerSiguienteConsecutivoAsync(fincaCodigo, cancellationToken);

        return Ok(new { Siguiente_Consecutivo = consecutivo });
    }


    [HttpPost("validar")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> Validar(
        [FromServices] IValidator<ValidarCompraRequest> validator,
        [FromBody] ValidarCompraRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);

        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(HttpContext, validacion);
        }

        return Ok();
    }

    [HttpPost]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> Registrar(
        [FromServices] IValidator<RegistrarCompraRequest> validator,
        [FromBody] RegistrarCompraRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);

        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(HttpContext, validacion);
        }

        var exito = await service.CrearRegistroAsync(request, cancellationToken);

        return exito
            ? StatusCode(StatusCodes.Status201Created)
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }

    [HttpPost("lote")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> RegistrarLote(
        [FromServices] IValidator<RegistrarCompraLoteRequest> validator,
        [FromBody] RegistrarCompraLoteRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);

        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(HttpContext, validacion);
        }

        var exito = await service.RegistrarLoteAsync(request, cancellationToken);

        return exito
            ? StatusCode(StatusCodes.Status201Created)
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }
}
