using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.Business.API.ErrorHandling;
using Gestion.Ganadera.Business.API.Requests.Helpers;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.Procesos;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreProductivoMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/procesos/pesaje")]
[ControllerPermissions(ControllerPermission.Create | ControllerPermission.GetPaged)]
public class PesajeController(IPesajeService service) : ControllerBase
{
    [HttpPost("validar")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> Validar(
        [FromServices] IValidator<ValidarPesajeRequest> validator,
        [FromBody] ValidarPesajeRequest request,
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
        [FromServices] IValidator<ValidarPesajeLoteRequest> validator,
        [FromBody] ValidarPesajeLoteRequest request,
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
        [FromServices] IValidator<RegistrarPesajeRequest> validator,
        [FromBody] RegistrarPesajeRequest request,
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
        [FromServices] IValidator<RegistrarPesajeLoteRequest> validator,
        [FromBody] RegistrarPesajeLoteRequest request,
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