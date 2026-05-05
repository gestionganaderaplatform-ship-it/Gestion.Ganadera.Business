using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

/// <summary>
/// Representa el detalle específico de un evento de descarte de animal.
/// </summary>
public class EventoDetalleDescarte : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public EventoGanadero? Evento_Ganadero { get; set; }

    public long Descarte_Motivo_Codigo { get; set; }
    public DescarteMotivo? Descarte_Motivo { get; set; }

    public DateTime Evento_Detalle_Descarte_Fecha { get; set; }
    public string? Evento_Detalle_Descarte_Destino { get; set; }
    public decimal? Evento_Detalle_Descarte_Valor { get; set; }
    public string? Evento_Detalle_Descarte_Observacion { get; set; }
}
