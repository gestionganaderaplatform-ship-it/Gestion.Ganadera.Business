using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

/// <summary>
/// Representa un resultado estandarizado de una palpación o revisión reproductiva.
/// </summary>
public class PalpacionResultado : AuditableEntity
{
    public long Palpacion_Resultado_Codigo { get; set; }
    public string Palpacion_Resultado_Nombre { get; set; } = string.Empty;
    public bool Palpacion_Resultado_Activo { get; set; } = true;
}
