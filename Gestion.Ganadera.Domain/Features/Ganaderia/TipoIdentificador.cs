using Gestion.Ganadera.Domain.Base;

namespace Gestion.Ganadera.Domain.Features.Ganaderia;

public class TipoIdentificador : AuditableEntity
{
    public long Tipo_Identificador_Codigo { get; set; }
    public string Tipo_Identificador_Nombre { get; set; } = string.Empty;
    public string? Tipo_Identificador_Codigo_Interno { get; set; }
    public bool Tipo_Identificador_Operativo { get; set; } = true;
    public bool Tipo_Identificador_Permite_Busqueda { get; set; } = true;
    public bool Tipo_Identificador_Permite_Principal { get; set; } = true;
    public bool Tipo_Identificador_Activo { get; set; } = true;
}
