using AutoMapper;
using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;
using Microsoft.Extensions.Logging;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class CausaMuerteService(
    ICausaMuerteRepository repository,
    IMapper mapper)
    : BaseService<CausaMuerte, CausaMuerteViewModel, CausaMuerteCreateViewModel, CausaMuerteUpdateViewModel, ICausaMuerteRepository>(
        repository,
        mapper), ICausaMuerteService
{
}
