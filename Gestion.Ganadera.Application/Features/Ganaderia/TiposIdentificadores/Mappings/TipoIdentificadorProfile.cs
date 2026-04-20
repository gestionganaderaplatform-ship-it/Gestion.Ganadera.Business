using AutoMapper;
using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.ViewModels;
using TipoIdentificadorEntity = Gestion.Ganadera.Domain.Features.Ganaderia.TipoIdentificador;

namespace Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Mappings;

public class TipoIdentificadorProfile : Profile
{
    public TipoIdentificadorProfile()
    {
        CreateMap<TipoIdentificadorEntity, TipoIdentificadorViewModel>().ReverseMap();

        CreateMap<TipoIdentificadorCreateViewModel, TipoIdentificadorEntity>()
            .ForMember(dest => dest.Tipo_Identificador_Nombre, opt => opt.MapFrom(src => src.Tipo_Identificador_Nombre.Trim()));

        CreateMap<TipoIdentificadorUpdateViewModel, TipoIdentificadorEntity>()
            .ForMember(dest => dest.Tipo_Identificador_Nombre, opt => opt.MapFrom(src => src.Tipo_Identificador_Nombre.Trim()));
    }
}
