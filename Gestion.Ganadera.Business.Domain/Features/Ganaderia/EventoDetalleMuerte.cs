using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class EventoDetalleMuerte : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public DateTime Evento_Detalle_Muerte_Fecha { get; set; }
    public long Causa_Muerte_Codigo { get; set; }
    public string? Evento_Detalle_Muerte_Observacion { get; set; }
}