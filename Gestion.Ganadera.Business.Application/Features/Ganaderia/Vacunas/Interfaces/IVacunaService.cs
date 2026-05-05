using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using VacunaEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.Vacuna;
using VacunaViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.ViewModels.VacunaViewModel;
using VacunaCreateViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.ViewModels.VacunaCreateViewModel;
using VacunaUpdateViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.ViewModels.VacunaUpdateViewModel;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Interfaces;

public interface IVacunaService : IBaseService<VacunaViewModel, VacunaCreateViewModel, VacunaUpdateViewModel>
{
}
