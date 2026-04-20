using Gestion.Ganadera.Domain.Base;

namespace Gestion.Ganadera.Domain.Features.Ganaderia;

public class IdentificadorAnimal : AuditableEntity
{
    public long Identificador_Animal_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public long Tipo_Identificador_Codigo { get; set; }
    public string Identificador_Animal_Valor { get; set; } = string.Empty;
    public bool Identificador_Animal_Es_Principal { get; set; }
    public bool Identificador_Animal_Activo { get; set; } = true;
}
