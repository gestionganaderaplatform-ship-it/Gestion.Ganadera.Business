using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.Business.API.ErrorHandling;
using Gestion.Ganadera.Business.API.Requests.Helpers;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.Procesos;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreProductivoMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/procesos/destete")]
[ControllerPermissions(ControllerPermission.Create | ControllerPermission.GetPaged)]
public class DesteteController(IDesteteService service) : ControllerBase
{
    [HttpPost("validar")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> Validar(
        [FromServices] IValidator<ValidarDesteteRequest> validator,
        [FromBody] ValidarDesteteRequest request,
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
        [FromServices] IValidator<ValidarDesteteLoteRequest> validator,
        [FromBody] ValidarDesteteLoteRequest request,
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
        [FromServices] IValidator<RegistrarDesteteRequest> validator,
        [FromBody] RegistrarDesteteRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);

        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(HttpContext, validacion);
        }

        var exito = await service.RegistrarAsync(request, cancellationToken);

        return exito
            ? StatusCode(StatusCodes.Status201Created, DesteteMessages.DesteteRegistrado)
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }

    [HttpPost("lote")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> RegistrarLote(
        [FromServices] IValidator<RegistrarDesteteLoteRequest> validator,
        [FromBody] RegistrarDesteteLoteRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);

        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(HttpContext, validacion);
        }

        var exito = await service.RegistrarLoteAsync(request, cancellationToken);

        return exito
            ? StatusCode(StatusCodes.Status201Created, string.Format("Se registraron {0} destetes correctamente.", request.Items.Count))
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }
}
