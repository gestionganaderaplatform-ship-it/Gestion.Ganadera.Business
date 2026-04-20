using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.API.Controllers.Base;
using Gestion.Ganadera.API.Security.Permissions;
using Gestion.Ganadera.API.Security.Planes;
using Gestion.Ganadera.Application.Features.Ganaderia.Potreros.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Potreros.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.API.Controllers.Ganaderia.Potreros;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreProductivoMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/potreros")]
[ControllerPermissions(
   ControllerPermission.GetPaged
   | ControllerPermission.GetAll
   | ControllerPermission.GetById
   | ControllerPermission.Create
   | ControllerPermission.Update
   | ControllerPermission.Delete
   | ControllerPermission.Exists
   | ControllerPermission.ExistsMany
   | ControllerPermission.Filter)]
public class PotreroController(
    IPotreroService service,
    ILogger<PotreroController> logger)
    : BaseController<PotreroViewModel, PotreroCreateViewModel, PotreroUpdateViewModel>(
        service,
        logger)
{
}
