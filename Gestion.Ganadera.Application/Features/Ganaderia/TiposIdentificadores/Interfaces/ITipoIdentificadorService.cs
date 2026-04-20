using Gestion.Ganadera.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.ViewModels;

namespace Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;

public interface ITipoIdentificadorService
    : IBaseService<TipoIdentificadorViewModel, TipoIdentificadorCreateViewModel, TipoIdentificadorUpdateViewModel>
{
}
