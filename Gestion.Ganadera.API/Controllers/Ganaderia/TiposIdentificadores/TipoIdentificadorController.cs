using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.API.Controllers.Base;
using Gestion.Ganadera.API.Security.Permissions;
using Gestion.Ganadera.API.Security.Planes;
using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.API.Controllers.Ganaderia.TiposIdentificadores;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/tipos-identificadores")]
[ControllerPermissions(
   ControllerPermission.GetAll)]
public class TipoIdentificadorController(
    ITipoIdentificadorService service,
    ILogger<TipoIdentificadorController> logger)
    : BaseController<TipoIdentificadorViewModel, TipoIdentificadorCreateViewModel, TipoIdentificadorUpdateViewModel>(
        service,
        logger)
{
}
