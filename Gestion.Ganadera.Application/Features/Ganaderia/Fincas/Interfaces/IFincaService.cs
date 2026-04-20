using Gestion.Ganadera.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.ViewModels;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Interfaces;

public interface IFincaService
    : IBaseService<FincaViewModel, FincaCreateViewModel, FincaUpdateViewModel>
{
}
