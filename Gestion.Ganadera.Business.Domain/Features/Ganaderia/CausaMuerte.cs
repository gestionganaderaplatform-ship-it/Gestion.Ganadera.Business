using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class CausaMuerte : AuditableEntity
{
    public long Causa_Muerte_Codigo { get; set; }
    public string Causa_Muerte_Nombre { get; set; } = string.Empty;
    public string? Causa_Muerte_Descripcion { get; set; }
    public bool Causa_Muerte_Activa { get; set; } = true;
    public int? Causa_Muerte_Orden { get; set; }
}
