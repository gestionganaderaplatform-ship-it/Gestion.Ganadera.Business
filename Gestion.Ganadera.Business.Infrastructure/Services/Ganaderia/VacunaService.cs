using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class VacunaService(
    IVacunaRepository repository,
    IMapper mapper)
    : BaseService<Vacuna, VacunaViewModel, VacunaCreateViewModel, VacunaUpdateViewModel, IVacunaRepository>(repository, mapper), IVacunaService
{
}