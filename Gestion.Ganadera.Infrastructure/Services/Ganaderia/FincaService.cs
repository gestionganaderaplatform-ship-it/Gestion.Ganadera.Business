using AutoMapper;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.ViewModels;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia;

public class FincaService(
    IFincaRepository repository,
    IMapper mapper)
    : BaseService<Finca, FincaViewModel, FincaCreateViewModel, FincaUpdateViewModel, IFincaRepository>(repository, mapper), IFincaService
{
}
