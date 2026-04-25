using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.Business.API.ErrorHandling;
using Gestion.Ganadera.Business.API.Requests.Helpers;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.Procesos;

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

    [HttpGet("siguiente-consecutivo")]
    [RequirePermission(ControllerPermission.GetPaged)]
    public async Task<IActionResult> ObtenerSiguienteConsecutivo(
        [FromQuery] long fincaCodigo,
        CancellationToken cancellationToken = default)
    {
        var siguienteConsecutivo = await service.ObtenerSiguienteConsecutivoAsync(fincaCodigo, cancellationToken);
        return Ok(new { Siguiente_Consecutivo = siguienteConsecutivo });
    }

    [HttpGet("existe-identificadores")]
    [RequirePermission(ControllerPermission.GetPaged)]
    public async Task<IActionResult> VerificarIdentificadores(
        [FromQuery] long fincaCodigo,
        [FromQuery] string identificadores,
        CancellationToken cancellationToken = default)
    {
        var lista = identificadores
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();

        var resultados = await service.ExistenIdentificadoresAsync(fincaCodigo, lista, cancellationToken);
        return Ok(new { Resultados = resultados });
    }
}
