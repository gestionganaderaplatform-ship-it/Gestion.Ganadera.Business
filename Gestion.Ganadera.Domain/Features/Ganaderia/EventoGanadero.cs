using Gestion.Ganadera.Domain.Base;

namespace Gestion.Ganadera.Domain.Features.Ganaderia;

public class EventoGanadero : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public long Finca_Codigo { get; set; }
    public string Evento_Ganadero_Tipo { get; set; } = string.Empty;
    public DateTime Evento_Ganadero_Fecha { get; set; }
    public DateTime Evento_Ganadero_Fecha_Registro { get; set; }
    public string Evento_Ganadero_Registrado_Por { get; set; } = string.Empty;
    public string Evento_Ganadero_Estado { get; set; } = string.Empty;
    public long? Evento_Ganadero_Origen_Codigo { get; set; }
    public bool Evento_Ganadero_Es_Correccion { get; set; }
    public bool Evento_Ganadero_Es_Anulacion { get; set; }
    public string? Evento_Ganadero_Observacion { get; set; }
}
