using Asp.Versioning;
using Gestion.Ganadera.Business.API.Controllers.Base;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.Palpaciones;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/palpaciones-resultados")]
[ControllerPermissions(ControllerPermission.GetAll | ControllerPermission.Create | ControllerPermission.Update)]
public class PalpacionResultadoController(
    IPalpacionResultadoService service,
    ILogger<PalpacionResultadoController> logger)
    : BaseController<PalpacionResultadoViewModel, PalpacionResultadoCreateViewModel, PalpacionResultadoUpdateViewModel>(
        service,
        logger)
{
}
