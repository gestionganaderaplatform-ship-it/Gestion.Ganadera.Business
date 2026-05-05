using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

/// <summary>
/// Representa el catálogo maestro de motivos por los cuales se descarta un animal.
/// </summary>
public class DescarteMotivo : AuditableEntity
{
    public long Descarte_Motivo_Codigo { get; set; }
    public string Descarte_Motivo_Nombre { get; set; } = string.Empty;
    public bool Descarte_Motivo_Activo { get; set; } = true;
}
