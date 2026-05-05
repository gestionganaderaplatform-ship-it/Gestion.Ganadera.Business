using Asp.Versioning;
using Gestion.Ganadera.Business.API.Controllers.Base;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.TratamientoProductos;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/tratamientos-productos")]
[ControllerPermissions(ControllerPermission.GetAll | ControllerPermission.Create | ControllerPermission.Update)]
public class TratamientoProductoController(
    ITratamientoProductoService service,
    ILogger<TratamientoProductoController> logger)
    : BaseController<TratamientoProductoViewModel, TratamientoProductoCreateViewModel, TratamientoProductoUpdateViewModel>(
        service,
        logger)
{
}
