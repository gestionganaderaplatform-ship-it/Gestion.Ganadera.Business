using AutoMapper;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.ViewModels;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia;

public class RangoEdadService(
    IRangoEdadRepository repository,
    IMapper mapper)
    : BaseService<RangoEdad, RangoEdadViewModel, RangoEdadCreateViewModel, RangoEdadUpdateViewModel, IRangoEdadRepository>(repository, mapper), IRangoEdadService
{
}
