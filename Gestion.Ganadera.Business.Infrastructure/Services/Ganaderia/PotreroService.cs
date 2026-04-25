using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class PotreroService(
    IPotreroRepository repository,
    IMapper mapper)
    : BaseService<Potrero, PotreroViewModel, PotreroCreateViewModel, PotreroUpdateViewModel, IPotreroRepository>(repository, mapper), IPotreroService
{
}
