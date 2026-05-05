using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class EventoDetalleVacunacion : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public DateTime Evento_Detalle_Vacunacion_Fecha { get; set; }
    public long Evento_Detalle_Vacunacion_Vacuna_Codigo { get; set; }
    public long? Evento_Detalle_Vacunacion_Enfermedad_Codigo { get; set; }
    public string Evento_Detalle_Vacunacion_Ciclo { get; set; } = string.Empty;
    public string Evento_Detalle_Vacunacion_Lote { get; set; } = string.Empty;
    public string? Evento_Detalle_Vacunacion_Vacunador { get; set; }
    public string? Evento_Detalle_Vacunacion_Dosis { get; set; }
    public string? Evento_Detalle_Vacunacion_Soporte_Nombre { get; set; }
    public string? Evento_Detalle_Vacunacion_Observacion { get; set; }
}