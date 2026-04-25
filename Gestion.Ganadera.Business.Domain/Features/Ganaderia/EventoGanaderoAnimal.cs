using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class EventoGanaderoAnimal : AuditableEntity
{
    public long Evento_Ganadero_Animal_Codigo { get; set; }
    public long Evento_Ganadero_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public int? Evento_Ganadero_Animal_Orden { get; set; }
    public string Evento_Ganadero_Animal_Estado_Afectacion { get; set; } = string.Empty;
    public string? Evento_Ganadero_Animal_Observacion { get; set; }
}
