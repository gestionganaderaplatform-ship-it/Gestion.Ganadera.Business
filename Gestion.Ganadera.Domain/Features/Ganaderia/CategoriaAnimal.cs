using Gestion.Ganadera.Domain.Base;

namespace Gestion.Ganadera.Domain.Features.Ganaderia;

public class CategoriaAnimal : AuditableEntity
{
    public long Categoria_Animal_Codigo { get; set; }
    public string Categoria_Animal_Nombre { get; set; } = string.Empty;
    public string? Categoria_Animal_Sexo_Esperado { get; set; }
    public int Categoria_Animal_Orden { get; set; }
    public bool Categoria_Animal_Activa { get; set; } = true;
}
