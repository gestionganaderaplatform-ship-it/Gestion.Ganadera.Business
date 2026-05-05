using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.ViewModels;
using TratamientoTipoEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.TratamientoTipo;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.Mappings;

public class TratamientoTipoProfile : Profile
{
    public TratamientoTipoProfile()
    {
        CreateMap<TratamientoTipoEntity, TratamientoTipoViewModel>()
            .ReverseMap();

        CreateMap<TratamientoTipoCreateViewModel, TratamientoTipoEntity>()
            .ForMember(dest => dest.Tratamiento_Tipo_Nombre, opt => opt.MapFrom(src => src.Tratamiento_Tipo_Nombre.Trim()));

        CreateMap<TratamientoTipoUpdateViewModel, TratamientoTipoEntity>()
            .ForMember(dest => dest.Tratamiento_Tipo_Nombre, opt => opt.MapFrom(src => src.Tratamiento_Tipo_Nombre.Trim()));
    }
}
