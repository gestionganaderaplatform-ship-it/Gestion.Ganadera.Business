using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.API.Controllers.Base;
using Gestion.Ganadera.API.Security.Permissions;
using Gestion.Ganadera.API.Security.Planes;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.API.Controllers.Ganaderia.RangosEdades;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/rangos-edades")]
[ControllerPermissions(
   ControllerPermission.None)]
public class RangoEdadController(
    IRangoEdadService service,
    ILogger<RangoEdadController> logger)
    : BaseController<RangoEdadViewModel, RangoEdadCreateViewModel, RangoEdadUpdateViewModel>(
        service,
        logger)
{
}
