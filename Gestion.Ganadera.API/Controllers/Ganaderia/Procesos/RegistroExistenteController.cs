using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.API.ErrorHandling;
using Gestion.Ganadera.API.Requests.Helpers;
using Gestion.Ganadera.API.Security.Permissions;
using Gestion.Ganadera.API.Security.Planes;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.API.Controllers.Ganaderia.Procesos;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreProductivoMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/procesos/registro-existente")]
[ControllerPermissions(ControllerPermission.Create)]
public class RegistroExistenteController : ControllerBase
{
    [HttpPost("validar")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> Validar(
        [FromServices] IValidator<ValidarRegistroExistenteRequest> validator,
        [FromBody] ValidarRegistroExistenteRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);
        
        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(
                 HttpContext,
                 validacion
             );
        }

        return Ok();
    }

    [HttpPost]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> Registrar(
        [FromServices] IValidator<RegistrarExistenteRequest> validator,
        [FromServices] Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces.IRegistroExistenteService service,
        [FromBody] RegistrarExistenteRequest request,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);
        
        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(
                 HttpContext,
                 validacion
             );
        }

        var exito = await service.CrearRegistroAsync(request, cancellationToken);

        return exito 
            ? StatusCode(StatusCodes.Status201Created)
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }
}
