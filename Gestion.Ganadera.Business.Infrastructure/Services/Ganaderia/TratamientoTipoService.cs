using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class TratamientoTipoService(
    ITratamientoTipoRepository repository,
    IMapper mapper)
    : BaseService<TratamientoTipo, TratamientoTipoViewModel, TratamientoTipoCreateViewModel, TratamientoTipoUpdateViewModel, ITratamientoTipoRepository>(repository, mapper), ITratamientoTipoService
{
}
