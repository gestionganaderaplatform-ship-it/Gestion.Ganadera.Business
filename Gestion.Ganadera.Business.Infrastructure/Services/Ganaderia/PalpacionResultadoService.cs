using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class PalpacionResultadoService(
    IPalpacionResultadoRepository repository,
    IMapper mapper)
    : BaseService<PalpacionResultado, PalpacionResultadoViewModel, PalpacionResultadoCreateViewModel, PalpacionResultadoUpdateViewModel, IPalpacionResultadoRepository>(repository, mapper), IPalpacionResultadoService
{
}
