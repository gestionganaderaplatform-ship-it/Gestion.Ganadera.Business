using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.ViewModels;
using CategoriaAnimalEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.CategoriaAnimal;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Mappings;

public class CategoriaAnimalProfile : Profile
{
    public CategoriaAnimalProfile()
    {
        CreateMap<CategoriaAnimalEntity, CategoriaAnimalViewModel>().ReverseMap();

        CreateMap<CategoriaAnimalCreateViewModel, CategoriaAnimalEntity>()
            .ForMember(dest => dest.Categoria_Animal_Nombre, opt => opt.MapFrom(src => src.Categoria_Animal_Nombre.Trim()));

        CreateMap<CategoriaAnimalUpdateViewModel, CategoriaAnimalEntity>()
            .ForMember(dest => dest.Categoria_Animal_Nombre, opt => opt.MapFrom(src => src.Categoria_Animal_Nombre.Trim()));
    }
}
