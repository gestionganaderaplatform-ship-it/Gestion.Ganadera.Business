using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.Mappings;

public class DescarteMotivoProfile : Profile
{
    public DescarteMotivoProfile()
    {
        CreateMap<DescarteMotivo, DescarteMotivoViewModel>();
        CreateMap<CreateDescarteMotivoViewModel, DescarteMotivo>();
        CreateMap<UpdateDescarteMotivoViewModel, DescarteMotivo>();
    }
}
