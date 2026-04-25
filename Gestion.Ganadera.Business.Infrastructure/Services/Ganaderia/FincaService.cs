using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class FincaService(
    IFincaRepository repository,
    IMapper mapper)
    : BaseService<Finca, FincaViewModel, FincaCreateViewModel, FincaUpdateViewModel, IFincaRepository>(repository, mapper), IFincaService
{
}
