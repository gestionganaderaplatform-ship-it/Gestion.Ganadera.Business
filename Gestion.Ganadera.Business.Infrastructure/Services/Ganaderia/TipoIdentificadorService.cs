using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class TipoIdentificadorService(
    ITipoIdentificadorRepository repository,
    IMapper mapper)
    : BaseService<TipoIdentificador, TipoIdentificadorViewModel, TipoIdentificadorCreateViewModel, TipoIdentificadorUpdateViewModel, ITipoIdentificadorRepository>(repository, mapper), ITipoIdentificadorService
{
}
