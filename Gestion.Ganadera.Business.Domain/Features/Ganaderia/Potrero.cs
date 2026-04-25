using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class Potrero : AuditableEntity
{
    public long Potrero_Codigo { get; set; }
    public long Finca_Codigo { get; set; }
    public string Potrero_Nombre { get; set; } = string.Empty;
    public bool Potrero_Activo { get; set; } = true;
}
