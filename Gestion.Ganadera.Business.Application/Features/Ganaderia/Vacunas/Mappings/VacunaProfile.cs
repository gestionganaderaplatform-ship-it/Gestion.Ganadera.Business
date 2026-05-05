using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.ViewModels;
using VacunaEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.Vacuna;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Mappings;

public class VacunaProfile : Profile
{
    public VacunaProfile()
    {
        CreateMap<VacunaEntity, VacunaViewModel>()
            .ForMember(dest => dest.Vacuna_Enfermedad_Nombre, opt => opt.MapFrom(src => src.Vacuna_Enfermedad != null ? src.Vacuna_Enfermedad.Vacuna_Enfermedad_Nombre : null))
            .ReverseMap();

        CreateMap<VacunaCreateViewModel, VacunaEntity>()
            .ForMember(dest => dest.Vacuna_Nombre, opt => opt.MapFrom(src => src.Vacuna_Nombre.Trim()));

        CreateMap<VacunaUpdateViewModel, VacunaEntity>()
            .ForMember(dest => dest.Vacuna_Nombre, opt => opt.MapFrom(src => src.Vacuna_Nombre.Trim()));
    }
}