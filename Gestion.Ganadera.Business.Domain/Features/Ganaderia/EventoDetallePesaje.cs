using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class EventoDetallePesaje : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public DateTime Evento_Detalle_Pesaje_Fecha { get; set; }
    public decimal Evento_Detalle_Peso { get; set; }
    public string? Evento_Detalle_Pesaje_Observacion { get; set; }
}