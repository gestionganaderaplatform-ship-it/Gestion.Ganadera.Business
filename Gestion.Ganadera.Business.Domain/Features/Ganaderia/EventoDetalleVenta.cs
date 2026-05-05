using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class EventoDetalleVenta : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public DateTime Evento_Detalle_Venta_Fecha { get; set; }
    public string Evento_Detalle_Venta_Comprador { get; set; } = string.Empty;
    public decimal? Evento_Detalle_Venta_Valor { get; set; }
    public string? Evento_Detalle_Venta_Observacion { get; set; }
}