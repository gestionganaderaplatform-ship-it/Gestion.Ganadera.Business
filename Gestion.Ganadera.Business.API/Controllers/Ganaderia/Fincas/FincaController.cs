using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.Business.API.Controllers.Base;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.Fincas;

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
