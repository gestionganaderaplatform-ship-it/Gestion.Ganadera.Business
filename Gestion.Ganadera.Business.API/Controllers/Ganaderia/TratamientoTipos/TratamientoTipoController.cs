using Asp.Versioning;
using Gestion.Ganadera.Business.API.Controllers.Base;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.TratamientoTipos;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/tratamientos-tipos")]
[ControllerPermissions(ControllerPermission.GetAll | ControllerPermission.Create | ControllerPermission.Update)]
public class TratamientoTipoController(
    ITratamientoTipoService service,
    ILogger<TratamientoTipoController> logger)
    : BaseController<TratamientoTipoViewModel, TratamientoTipoCreateViewModel, TratamientoTipoUpdateViewModel>(
        service,
        logger)
{
}
