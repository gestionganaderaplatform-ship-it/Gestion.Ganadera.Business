using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

/// <summary>
/// Representa el detalle técnico de un evento de traslado de animales entre fincas de la misma cuenta.
/// </summary>
public class EventoDetalleTrasladoFinca : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public DateTime Evento_Detalle_Traslado_Finca_Fecha { get; set; }
    public long Finca_Codigo_Origen { get; set; }
    public long Finca_Codigo_Destino { get; set; }
    public long Potrero_Codigo_Destino { get; set; }
    public string? Evento_Detalle_Traslado_Finca_Observacion { get; set; }
}
