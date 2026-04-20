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
[Authorize(Policy = PoliticaPlan.CuentaPadreProductivoMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/fincas")]
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
public class FincaController(
    IFincaService service,
    ILogger<FincaController> logger)
    : BaseController<FincaViewModel, FincaCreateViewModel, FincaUpdateViewModel>(
        service,
        logger)
{
}
