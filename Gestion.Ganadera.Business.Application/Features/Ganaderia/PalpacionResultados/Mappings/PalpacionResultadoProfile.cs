using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.ViewModels;
using PalpacionResultadoEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.PalpacionResultado;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Mappings;

public class PalpacionResultadoProfile : Profile
{
    public PalpacionResultadoProfile()
    {
        CreateMap<PalpacionResultadoEntity, PalpacionResultadoViewModel>()
            .ReverseMap();

        CreateMap<PalpacionResultadoCreateViewModel, PalpacionResultadoEntity>()
            .ForMember(dest => dest.Palpacion_Resultado_Nombre, opt => opt.MapFrom(src => src.Palpacion_Resultado_Nombre.Trim()));

        CreateMap<PalpacionResultadoUpdateViewModel, PalpacionResultadoEntity>()
            .ForMember(dest => dest.Palpacion_Resultado_Nombre, opt => opt.MapFrom(src => src.Palpacion_Resultado_Nombre.Trim()));
    }
}
