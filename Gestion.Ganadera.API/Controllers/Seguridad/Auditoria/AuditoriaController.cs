using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gestion.Ganadera.API.Controllers.Base;
using Gestion.Ganadera.API.Security.Planes;
using Gestion.Ganadera.API.Security.Permissions;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Interfaces;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.ViewModels;

namespace Gestion.Ganadera.API.Controllers.Seguridad.Auditoria
{
    /// <summary>
    /// Expone consulta y administracion basica de los registros de auditoria del sistema.
    /// </summary>
    [ApiController]
    [Authorize(Policy = PoliticaPlan.CuentaPadreProductivoMinimo)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/seguridad/auditoria")]
    [ControllerPermissions(
       ControllerPermission.GetPaged
       | ControllerPermission.Filter
   | ControllerPermission.ExportExcel
   )]
    public class AuditoriaController(
        IAuditoriaService service,
        IValidator<AuditoriaExportFilterViewModel> exportValidator,
        ILogger<AuditoriaController> logger)
        : BaseController<AuditoriaViewModel, AuditoriaCreateViewModel, AuditoriaUpdateViewModel, AuditoriaExportFilterViewModel>(
            service,
            logger,
            exportValidator)
    {
    }
}
