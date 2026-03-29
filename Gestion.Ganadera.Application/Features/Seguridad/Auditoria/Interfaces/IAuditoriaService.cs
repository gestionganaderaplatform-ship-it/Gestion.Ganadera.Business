using Gestion.Ganadera.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.ViewModels;

namespace Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Interfaces
{
    public interface IAuditoriaService
        : IBaseService<AuditoriaViewModel, AuditoriaCreateViewModel, AuditoriaUpdateViewModel, AuditoriaExportFilterViewModel>
    {
    }
}
