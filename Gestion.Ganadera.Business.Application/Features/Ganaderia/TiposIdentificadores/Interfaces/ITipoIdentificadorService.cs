using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;

public interface ITipoIdentificadorService
    : IBaseService<TipoIdentificadorViewModel, TipoIdentificadorCreateViewModel, TipoIdentificadorUpdateViewModel>
{
}
