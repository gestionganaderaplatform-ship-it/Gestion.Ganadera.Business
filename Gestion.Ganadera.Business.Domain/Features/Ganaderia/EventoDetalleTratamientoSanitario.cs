using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

/// <summary>
/// Representa el detalle específico de un evento de tratamiento sanitario.
/// </summary>
public class EventoDetalleTratamientoSanitario : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public EventoGanadero? Evento_Ganadero { get; set; }

    public long Tratamiento_Producto_Codigo { get; set; }
    public TratamientoProducto? Tratamiento_Producto { get; set; }

    public DateTime Evento_Detalle_Tratamiento_Fecha { get; set; }
    
    public decimal? Evento_Detalle_Tratamiento_Dosis { get; set; }
    public string? Evento_Detalle_Tratamiento_Duracion { get; set; }
    public string? Evento_Detalle_Tratamiento_Indicacion { get; set; }
    public string? Evento_Detalle_Tratamiento_Aplicador { get; set; }
    public string? Evento_Detalle_Tratamiento_Observacion { get; set; }
}
