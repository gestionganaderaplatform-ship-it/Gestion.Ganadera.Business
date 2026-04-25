using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.API.Controllers.Base;
using Gestion.Ganadera.API.Security.Permissions;
using Gestion.Ganadera.API.Security.Planes;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.API.Controllers.Ganaderia.Fincas;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/fincas")]
[ControllerPermissions(
   ControllerPermission.GetAll
   | ControllerPermission.Create)]
public class FincaController(
    IFincaService service,
    ILogger<FincaController> logger)
    : BaseController<FincaViewModel, FincaCreateViewModel, FincaUpdateViewModel>(
        service,
        logger)
{
}
