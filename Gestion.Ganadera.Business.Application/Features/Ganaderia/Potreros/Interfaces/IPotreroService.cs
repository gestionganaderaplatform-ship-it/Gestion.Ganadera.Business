using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.Interfaces;

public interface IPotreroService
    : IBaseService<PotreroViewModel, PotreroCreateViewModel, PotreroUpdateViewModel>
{
}
