using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.Business.API.ErrorHandling;
using Gestion.Ganadera.Business.API.Requests.Helpers;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.Procesos;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreProductivoMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/procesos/movimiento-potrero")]
[ControllerPermissions(ControllerPermission.Create | ControllerPermission.GetPaged)]
public class MovimientoPotreroController(IMovimientoPotreroService service) : ControllerBase
{
    [HttpPost("validar")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> Validar(
        [FromServices] IValidator<ValidarMovimientoPotreroRequest> validator,
        [FromBody] ValidarMovimientoPotreroRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);

        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(HttpContext, validacion);
        }

        return Ok();
    }

    [HttpPost("validar-lote")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> ValidarLote(
        [FromServices] IValidator<ValidarMovimientoPotreroLoteRequest> validator,
        [FromBody] ValidarMovimientoPotreroLoteRequest request,
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
        [FromServices] IValidator<RegistrarMovimientoPotreroRequest> validator,
        [FromBody] RegistrarMovimientoPotreroRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);

        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(HttpContext, validacion);
        }

        var exito = await service.RegistrarAsync(request, cancellationToken);

        return exito
            ? StatusCode(StatusCodes.Status201Created, MovimientoPotreroMessages.MovimientoRegistrado)
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }

    [HttpPost("lote")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> RegistrarLote(
        [FromServices] IValidator<RegistrarMovimientoPotreroLoteRequest> validator,
        [FromBody] RegistrarMovimientoPotreroLoteRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);

        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(HttpContext, validacion);
        }

        var exito = await service.RegistrarLoteAsync(request, cancellationToken);

        return exito
            ? StatusCode(StatusCodes.Status201Created, string.Format(MovimientoPotreroMessages.MovimientoLoteRegistrado, request.Animales.Count))
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }
}
