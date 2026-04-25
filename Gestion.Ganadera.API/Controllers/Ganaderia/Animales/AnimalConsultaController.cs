using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.API.ErrorHandling;
using Gestion.Ganadera.API.ErrorHandling.Messages;
using Gestion.Ganadera.API.Extensions;
using Gestion.Ganadera.API.Requests.Helpers;
using Gestion.Ganadera.API.Security.Permissions;
using Gestion.Ganadera.API.Security.Planes;
using Gestion.Ganadera.Application.Features.Base.Models;
using Gestion.Ganadera.Application.Features.Ganaderia.Animales.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.API.Controllers.Ganaderia.Animales;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/animales-consulta")]
[ControllerPermissions(ControllerPermission.GetPaged | ControllerPermission.GetById)]
public class AnimalConsultaController(IAnimalConsultaService service) : ControllerBase
{
    [HttpGet("resumen-inicio")]
    [RequirePermission(ControllerPermission.GetPaged)]
    public async Task<IActionResult> ObtenerResumenInicio(
        [FromQuery] long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        var resumen = await service.ObtenerResumenInicioAsync(fincaCodigo, cancellationToken);
        return Ok(resumen);
    }

    [HttpGet("paginado")]
    [RequirePermission(ControllerPermission.GetPaged)]
    public async Task<IActionResult> ObtenerPorPaginado(
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanoPagina = 25,
        [FromQuery] long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        var (items, total) = await service.ObtenerPorPaginado(
            pagina,
            tamanoPagina,
            fincaCodigo,
            cancellationToken);

        return Ok(new { Items = items, TotalRegistros = total });
    }

    [HttpGet("{codigo}")]
    [RequirePermission(ControllerPermission.GetById)]
    public async Task<IActionResult> ConsultarPorCodigo(
        [FromServices] IValidator<CodigoRequest> validator,
        string codigo,
        [FromQuery] long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        var validacion = await ControllerValidatorHelper.ValidarCodigo(validator, codigo);
        if (validacion is not null)
        {
            return ApiProblemDetailsFactory.BadRequest(
                 HttpContext,
                 validacion
             );
        }

        var codigoNumerico = codigo.ToLong();
        var snapshot = await service.Consultar(codigoNumerico, fincaCodigo, cancellationToken);

        if (snapshot is null)
        {
            return ApiProblemDetailsFactory.NotFound(
                HttpContext,
                detail: ApiErrorMessages.RecordNotFound(codigoNumerico));
        }

        return Ok(snapshot);
    }

    [HttpGet("{codigo:long}/historial")]
    [RequirePermission(ControllerPermission.GetById)]
    public async Task<IActionResult> ObtenerHistorial(
        [FromRoute] long codigo,
        [FromQuery] long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        var historial = await service.ObtenerHistorialAsync(codigo, fincaCodigo, cancellationToken);
        return Ok(historial);
    }
}
