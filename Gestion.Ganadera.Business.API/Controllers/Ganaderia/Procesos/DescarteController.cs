using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.Business.API.ErrorHandling;
using Gestion.Ganadera.Business.API.Requests.Helpers;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.Procesos;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreProductivoMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/procesos/descarte")]
[ControllerPermissions(ControllerPermission.Create | ControllerPermission.GetPaged)]
public class DescarteController(IDescarteService service) : ControllerBase
{
    [HttpPost("validar")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> Validar(
        [FromServices] IValidator<ValidarDescarteRequest> validator,
        [FromBody] ValidarDescarteRequest request,
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
        [FromServices] IValidator<ValidarDescarteLoteRequest> validator,
        [FromBody] ValidarDescarteLoteRequest request,
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
        [FromServices] IValidator<RegistrarDescarteRequest> validator,
        [FromBody] RegistrarDescarteRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);
        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(HttpContext, validacion);
        }

        var result = await service.RegistrarAsync(request, cancellationToken);
        return result 
            ? StatusCode(StatusCodes.Status201Created)
            : ApiProblemDetailsFactory.BadRequest(HttpContext, detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }

    [HttpPost("lote")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> RegistrarLote(
        [FromServices] IValidator<RegistrarDescarteLoteRequest> validator,
        [FromBody] RegistrarDescarteLoteRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);
        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(HttpContext, validacion);
        }

        var result = await service.RegistrarLoteAsync(request, cancellationToken);
        return result 
            ? StatusCode(StatusCodes.Status201Created)
            : ApiProblemDetailsFactory.BadRequest(HttpContext, detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }
}
