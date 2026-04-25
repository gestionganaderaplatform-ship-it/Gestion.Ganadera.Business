using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.ViewModels;
using PotreroEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.Potrero;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.Mappings;

public class PotreroProfile : Profile
{
    public PotreroProfile()
    {
        CreateMap<PotreroEntity, PotreroViewModel>().ReverseMap();

        CreateMap<PotreroCreateViewModel, PotreroEntity>()
            .ForMember(dest => dest.Potrero_Nombre, opt => opt.MapFrom(src => src.Potrero_Nombre.Trim()));

        CreateMap<PotreroUpdateViewModel, PotreroEntity>()
            .ForMember(dest => dest.Potrero_Nombre, opt => opt.MapFrom(src => src.Potrero_Nombre.Trim()));
    }
}
