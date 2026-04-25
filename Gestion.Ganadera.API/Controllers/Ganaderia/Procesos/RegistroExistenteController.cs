using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.API.ErrorHandling;
using Gestion.Ganadera.API.Requests.Helpers;
using Gestion.Ganadera.API.Security.Permissions;
using Gestion.Ganadera.API.Security.Planes;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.API.Controllers.Ganaderia.Procesos;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/procesos/registro-existente")]
[ControllerPermissions(ControllerPermission.Create | ControllerPermission.GetPaged)]
public class RegistroExistenteController(IRegistroExistenteService service) : ControllerBase
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

        var exito = await service.RegistrarAsync(request, cancellationToken);

        return exito 
            ? StatusCode(StatusCodes.Status201Created)
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }

    [HttpPost("lote")]
    [RequirePermission(ControllerPermission.Create)]
    public async Task<IActionResult> RegistrarLote(
        [FromServices] IValidator<RegistrarExistenteLoteRequest> validator,
        [FromBody] RegistrarExistenteLoteRequest request,
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

        var exito = await service.RegistrarLoteAsync(request, cancellationToken);

        return exito 
            ? StatusCode(StatusCodes.Status201Created)
            : ApiProblemDetailsFactory.BadRequest(
                HttpContext,
                detail: API.ErrorHandling.Messages.ApiErrorMessages.OperationFailed);
    }

    [HttpGet("existe-identificador")]
    [RequirePermission(ControllerPermission.GetPaged)]
    public async Task<IActionResult> VerificarIdentificador(
        [FromQuery] long fincaCodigo,
        [FromQuery] string identificador,
        CancellationToken cancellationToken = default)
    {
        var existe = await service.ExisteIdentificadorAsync(fincaCodigo, identificador, cancellationToken);
        return Ok(new { Existe = existe });
    }

    [HttpGet("siguiente-consecutivo")]
    [RequirePermission(ControllerPermission.GetPaged)]
    public async Task<IActionResult> ObtenerSiguienteConsecutivo(
        [FromQuery] long fincaCodigo,
        CancellationToken cancellationToken = default)
    {
        var siguienteConsecutivo = await service.ObtenerSiguienteConsecutivoAsync(fincaCodigo, cancellationToken);
        return Ok(new { Siguiente_Consecutivo = siguienteConsecutivo });
    }
}
