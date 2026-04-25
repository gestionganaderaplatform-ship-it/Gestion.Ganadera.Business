using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.Business.API.Controllers.Base;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.TiposIdentificadores;

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
