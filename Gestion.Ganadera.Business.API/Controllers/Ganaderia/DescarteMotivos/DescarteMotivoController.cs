using Asp.Versioning;
using Gestion.Ganadera.Business.API.Controllers.Base;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.DescarteMotivos;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/catalogos/descarte-motivos")]
[ControllerPermissions(ControllerPermission.Standard)]
public class DescarteMotivoController(
    IDescarteMotivoService service,
    ILogger<DescarteMotivoController> logger)
    : BaseController<DescarteMotivoViewModel, CreateDescarteMotivoViewModel, UpdateDescarteMotivoViewModel>(service, logger)
{
}
