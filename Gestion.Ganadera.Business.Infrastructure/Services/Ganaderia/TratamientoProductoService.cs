using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class TratamientoProductoService(
    ITratamientoProductoRepository repository,
    IMapper mapper)
    : BaseService<TratamientoProducto, TratamientoProductoViewModel, TratamientoProductoCreateViewModel, TratamientoProductoUpdateViewModel, ITratamientoProductoRepository>(repository, mapper), ITratamientoProductoService
{
}
