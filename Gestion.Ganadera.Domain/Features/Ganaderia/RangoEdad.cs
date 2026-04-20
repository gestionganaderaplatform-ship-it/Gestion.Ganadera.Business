using Gestion.Ganadera.Domain.Base;

namespace Gestion.Ganadera.Domain.Features.Ganaderia;

public class RangoEdad : AuditableEntity
{
    public long Rango_Edad_Codigo { get; set; }
    public string Rango_Edad_Nombre { get; set; } = string.Empty;
    public int? Rango_Edad_Edad_Minima_Meses { get; set; }
    public int? Rango_Edad_Edad_Maxima_Meses { get; set; }
    public int Rango_Edad_Orden { get; set; }
    public bool Rango_Edad_Activo { get; set; } = true;
}
