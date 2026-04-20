using AutoMapper;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.ViewModels;
using FincaEntity = Gestion.Ganadera.Domain.Features.Ganaderia.Finca;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Mappings;

public class FincaProfile : Profile
{
    public FincaProfile()
    {
        CreateMap<FincaEntity, FincaViewModel>().ReverseMap();

        CreateMap<FincaCreateViewModel, FincaEntity>()
            .ForMember(dest => dest.Finca_Nombre, opt => opt.MapFrom(src => src.Finca_Nombre.Trim()));

        CreateMap<FincaUpdateViewModel, FincaEntity>()
            .ForMember(dest => dest.Finca_Nombre, opt => opt.MapFrom(src => src.Finca_Nombre.Trim()));
    }
}
