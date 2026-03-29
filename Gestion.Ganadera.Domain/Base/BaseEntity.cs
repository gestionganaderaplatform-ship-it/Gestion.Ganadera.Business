namespace Gestion.Ganadera.Domain.Base
{
    /// <summary>
    /// Base minima para entidades del dominio con identificadores compartidos por la plantilla.
    /// </summary>
    public class BaseEntity
    {
        public Guid Codigo_Publico { get; set; } = Guid.NewGuid();
        public Guid Codigo_Cliente { get; set; }
    }
}

