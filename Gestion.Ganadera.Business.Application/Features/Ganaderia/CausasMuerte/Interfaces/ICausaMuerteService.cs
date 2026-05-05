using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Interfaces;

public interface ICausaMuerteService
    : IBaseService<CausaMuerteViewModel, CausaMuerteCreateViewModel, CausaMuerteUpdateViewModel>
{
}
