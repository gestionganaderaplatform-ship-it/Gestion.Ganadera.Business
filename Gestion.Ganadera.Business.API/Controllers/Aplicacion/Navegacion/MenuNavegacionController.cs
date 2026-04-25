using Asp.Versioning;
using Gestion.Ganadera.Business.Application.Features.Navegacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.Business.API.Controllers.Aplicacion.Navegacion
{
    /// <summary>
    /// Entrega el menu de navegacion visible para el usuario autenticado.
    /// </summary>
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/aplicacion/navegacion/menu")]
    public class MenuNavegacionController(IMenuNavegacionService menuNavegacionService) : ControllerBase
    {
        private readonly IMenuNavegacionService _menuNavegacionService = menuNavegacionService;

        [HttpGet]
        public async Task<IActionResult> Obtener(CancellationToken cancellationToken)
        {
            var menu = await _menuNavegacionService.ObtenerMenuActualAsync(cancellationToken);
            return Ok(menu);
        }
    }
}
