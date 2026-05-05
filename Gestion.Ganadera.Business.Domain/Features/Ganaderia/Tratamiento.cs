using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

/// <summary>
/// Representa el producto o fármaco utilizado en tratamientos sanitarios.
/// </summary>
public class TratamientoProducto : AuditableEntity
{
    public long Tratamiento_Producto_Codigo { get; set; }
    public string Tratamiento_Producto_Nombre { get; set; } = string.Empty;
    public long Tratamiento_Tipo_Codigo { get; set; }
    public TratamientoTipo? Tratamiento_Tipo { get; set; }
    public bool Tratamiento_Producto_Activo { get; set; } = true;
}

/// <summary>
/// Representa la categoría o tipo de tratamiento (Antibiótico, Analgésico, etc.).
/// </summary>
public class TratamientoTipo : AuditableEntity
{
    public long Tratamiento_Tipo_Codigo { get; set; }
    public string Tratamiento_Tipo_Nombre { get; set; } = string.Empty;
    public bool Tratamiento_Tipo_Activa { get; set; } = true;
}
