using AutoMapper;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.ViewModels;
using RangoEdadEntity = Gestion.Ganadera.Domain.Features.Ganaderia.RangoEdad;

namespace Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Mappings;

public class RangoEdadProfile : Profile
{
    public RangoEdadProfile()
    {
        CreateMap<RangoEdadEntity, RangoEdadViewModel>().ReverseMap();

        CreateMap<RangoEdadCreateViewModel, RangoEdadEntity>()
            .ForMember(dest => dest.Rango_Edad_Nombre, opt => opt.MapFrom(src => src.Rango_Edad_Nombre.Trim()));

        CreateMap<RangoEdadUpdateViewModel, RangoEdadEntity>()
            .ForMember(dest => dest.Rango_Edad_Nombre, opt => opt.MapFrom(src => src.Rango_Edad_Nombre.Trim()));
    }
}
