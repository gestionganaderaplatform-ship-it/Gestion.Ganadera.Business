using Asp.Versioning;
using Gestion.Ganadera.Business.API.Controllers.Base;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.Vacunas;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/vacunas")]
[ControllerPermissions(
   ControllerPermission.GetAll)]
public class VacunaController(
    IVacunaService service,
    ILogger<VacunaController> logger)
    : BaseController<VacunaViewModel, VacunaCreateViewModel, VacunaUpdateViewModel>(
        service,
        logger)
{
}
