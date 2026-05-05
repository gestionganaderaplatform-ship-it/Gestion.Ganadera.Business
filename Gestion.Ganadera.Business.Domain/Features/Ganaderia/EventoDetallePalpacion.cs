using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

/// <summary>
/// Representa el detalle específico de un evento de palpación o revisión reproductiva.
/// </summary>
public class EventoDetallePalpacion : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public EventoGanadero? Evento_Ganadero { get; set; }

    public long Palpacion_Resultado_Codigo { get; set; }
    public PalpacionResultado? Palpacion_Resultado { get; set; }

    public DateTime Evento_Detalle_Palpacion_Fecha { get; set; }
    public string? Evento_Detalle_Palpacion_Responsable { get; set; }
    public string? Evento_Detalle_Palpacion_Dato_Complementario { get; set; }
    public string? Evento_Detalle_Palpacion_Observacion { get; set; }
}
