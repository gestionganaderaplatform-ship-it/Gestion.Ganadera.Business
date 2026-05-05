using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.ViewModels;
using TratamientoProductoEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.TratamientoProducto;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.Mappings;

public class TratamientoProductoProfile : Profile
{
    public TratamientoProductoProfile()
    {
        CreateMap<TratamientoProductoEntity, TratamientoProductoViewModel>()
            .ForMember(dest => dest.Tratamiento_Tipo_Nombre, opt => opt.MapFrom(src => src.Tratamiento_Tipo != null ? src.Tratamiento_Tipo.Tratamiento_Tipo_Nombre : null))
            .ReverseMap();

        CreateMap<TratamientoProductoCreateViewModel, TratamientoProductoEntity>()
            .ForMember(dest => dest.Tratamiento_Producto_Nombre, opt => opt.MapFrom(src => src.Tratamiento_Producto_Nombre.Trim()));

        CreateMap<TratamientoProductoUpdateViewModel, TratamientoProductoEntity>()
            .ForMember(dest => dest.Tratamiento_Producto_Nombre, opt => opt.MapFrom(src => src.Tratamiento_Producto_Nombre.Trim()));
    }
}
