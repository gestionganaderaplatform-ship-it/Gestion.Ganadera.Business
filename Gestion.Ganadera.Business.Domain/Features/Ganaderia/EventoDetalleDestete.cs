using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

/// <summary>
/// Representa el detalle específico de un evento de destete.
/// </summary>
public class EventoDetalleDestete : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public EventoGanadero? Evento_Ganadero { get; set; }

    public long Animal_Codigo_Madre { get; set; }
    public long? Potrero_Destino_Codigo { get; set; }

    public DateTime Evento_Detalle_Destete_Fecha { get; set; }
    public string? Evento_Detalle_Destete_Responsable { get; set; }
    public string? Evento_Detalle_Destete_Observacion { get; set; }
}
