using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Seguridad.Auditoria.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Seguridad.Auditoria.Interfaces
{
    public interface IAuditoriaService
        : IBaseService<AuditoriaViewModel, AuditoriaCreateViewModel, AuditoriaUpdateViewModel, AuditoriaExportFilterViewModel>
    {
    }
}
