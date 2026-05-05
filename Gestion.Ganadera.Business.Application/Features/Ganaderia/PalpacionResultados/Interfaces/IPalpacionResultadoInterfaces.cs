using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using PalpacionResultadoEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.PalpacionResultado;
using PalpacionResultadoViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.ViewModels.PalpacionResultadoViewModel;
using PalpacionResultadoCreateViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.ViewModels.PalpacionResultadoCreateViewModel;
using PalpacionResultadoUpdateViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.ViewModels.PalpacionResultadoUpdateViewModel;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Interfaces;

public interface IPalpacionResultadoRepository : IBaseRepository<PalpacionResultadoEntity>
{
    Task<bool> ExisteNombreAsync(
        string nombre,
        long? codigoExcluir = null,
        CancellationToken cancellationToken = default);
}

public interface IPalpacionResultadoService : IBaseService<PalpacionResultadoViewModel, PalpacionResultadoCreateViewModel, PalpacionResultadoUpdateViewModel>
{
}
