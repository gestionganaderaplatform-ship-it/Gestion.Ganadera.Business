using AutoMapper;
using Gestion.Ganadera.Application.Features.Ganaderia.Potreros.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Potreros.ViewModels;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia;

public class PotreroService(
    IPotreroRepository repository,
    IMapper mapper)
    : BaseService<Potrero, PotreroViewModel, PotreroCreateViewModel, PotreroUpdateViewModel, IPotreroRepository>(repository, mapper), IPotreroService
{
}
