using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.ViewModels;

public class TratamientoProductoViewModel : IMapsToEntity<TratamientoProducto>
{
    public long Tratamiento_Producto_Codigo { get; set; }
    public string Tratamiento_Producto_Nombre { get; set; } = string.Empty;
    public long Tratamiento_Tipo_Codigo { get; set; }
    public string? Tratamiento_Tipo_Nombre { get; set; }
    public bool Tratamiento_Producto_Activo { get; set; }
}

public class TratamientoProductoCreateViewModel : IMapsToEntity<TratamientoProducto>
{
    public string Tratamiento_Producto_Nombre { get; set; } = string.Empty;
    public long Tratamiento_Tipo_Codigo { get; set; }
}

public class TratamientoProductoUpdateViewModel : IMapsToEntity<TratamientoProducto>
{
    public long Tratamiento_Producto_Codigo { get; set; }
    public string Tratamiento_Producto_Nombre { get; set; } = string.Empty;
    public long Tratamiento_Tipo_Codigo { get; set; }
    public bool Tratamiento_Producto_Activo { get; set; }
}
