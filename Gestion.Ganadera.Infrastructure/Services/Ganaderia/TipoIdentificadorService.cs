using AutoMapper;
using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.ViewModels;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia;

public class TipoIdentificadorService(
    ITipoIdentificadorRepository repository,
    IMapper mapper)
    : BaseService<TipoIdentificador, TipoIdentificadorViewModel, TipoIdentificadorCreateViewModel, TipoIdentificadorUpdateViewModel, ITipoIdentificadorRepository>(repository, mapper), ITipoIdentificadorService
{
}
