using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using TratamientoTipoEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.TratamientoTipo;
using TratamientoTipoViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.ViewModels.TratamientoTipoViewModel;
using TratamientoTipoCreateViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.ViewModels.TratamientoTipoCreateViewModel;
using TratamientoTipoUpdateViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.ViewModels.TratamientoTipoUpdateViewModel;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.Interfaces;

public interface ITratamientoTipoRepository : IBaseRepository<TratamientoTipoEntity>
{
    Task<bool> ExisteNombreAsync(
        string nombre,
        long? codigoExcluir = null,
        CancellationToken cancellationToken = default);
}

public interface ITratamientoTipoService : IBaseService<TratamientoTipoViewModel, TratamientoTipoCreateViewModel, TratamientoTipoUpdateViewModel>
{
}
