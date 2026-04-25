using Asp.Versioning;
using FluentValidation;
using Gestion.Ganadera.Business.API.Controllers.Base;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Gestion.Ganadera.Business.API.Security.Planes;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Ganaderia.CategoriasAnimales;

[ApiController]
[Authorize(Policy = PoliticaPlan.CuentaPadreEsencialMinimo)]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/ganaderia/categorias-animales")]
[ControllerPermissions(
   ControllerPermission.GetAll)]
public class CategoriaAnimalController(
    ICategoriaAnimalService service,
    ILogger<CategoriaAnimalController> logger)
    : BaseController<CategoriaAnimalViewModel, CategoriaAnimalCreateViewModel, CategoriaAnimalUpdateViewModel>(
        service,
        logger)
{
}
