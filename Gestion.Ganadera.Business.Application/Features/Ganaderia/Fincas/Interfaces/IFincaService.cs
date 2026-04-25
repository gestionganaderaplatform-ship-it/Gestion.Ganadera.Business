using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.Interfaces;

public interface IFincaService
    : IBaseService<FincaViewModel, FincaCreateViewModel, FincaUpdateViewModel>
{
}
