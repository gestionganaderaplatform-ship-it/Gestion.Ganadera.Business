using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.RangosEdades.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.RangosEdades.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class RangoEdadService(
    IRangoEdadRepository repository,
    IMapper mapper)
    : BaseService<RangoEdad, RangoEdadViewModel, RangoEdadCreateViewModel, RangoEdadUpdateViewModel, IRangoEdadRepository>(repository, mapper), IRangoEdadService
{
}
