using Gestion.Ganadera.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Potreros.ViewModels;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Potreros.Interfaces;

public interface IPotreroService
    : IBaseService<PotreroViewModel, PotreroCreateViewModel, PotreroUpdateViewModel>
{
}
