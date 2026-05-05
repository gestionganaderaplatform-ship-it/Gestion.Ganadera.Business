using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using TratamientoProductoEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.TratamientoProducto;
using TratamientoProductoViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.ViewModels.TratamientoProductoViewModel;
using TratamientoProductoCreateViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.ViewModels.TratamientoProductoCreateViewModel;
using TratamientoProductoUpdateViewModel = Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.ViewModels.TratamientoProductoUpdateViewModel;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.Interfaces;

public interface ITratamientoProductoRepository : IBaseRepository<TratamientoProductoEntity>
{
    Task<bool> ExisteNombreAsync(
        string nombre,
        long? codigoExcluir = null,
        CancellationToken cancellationToken = default);
}

public interface ITratamientoProductoService : IBaseService<TratamientoProductoViewModel, TratamientoProductoCreateViewModel, TratamientoProductoUpdateViewModel>
{
}
