using Asp.Versioning;
using Gestion.Ganadera.Business.API.Controllers.Base;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.CausasMuerte;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/causas-muerte")]
[ControllerPermissions(ControllerPermission.Standard)]
public class CausaMuerteController(
    ICausaMuerteService service,
    ILogger<CausaMuerteController> logger)
    : BaseController<CausaMuerteViewModel, CausaMuerteCreateViewModel, CausaMuerteUpdateViewModel>(
        service,
        logger)
{
}
