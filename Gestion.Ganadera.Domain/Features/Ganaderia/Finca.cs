using Gestion.Ganadera.Domain.Base;

namespace Gestion.Ganadera.Domain.Features.Ganaderia;

public class Finca : AuditableEntity
{
    public long Finca_Codigo { get; set; }
    public string Finca_Nombre { get; set; } = string.Empty;
    public bool Finca_Activa { get; set; } = true;
}
