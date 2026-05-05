using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class DescarteMotivoService(
    IDescarteMotivoRepository repository,
    IMapper mapper) 
    : BaseService<DescarteMotivo, DescarteMotivoViewModel, CreateDescarteMotivoViewModel, UpdateDescarteMotivoViewModel, IDescarteMotivoRepository>(repository, mapper), IDescarteMotivoService
{
}
