namespace Gestion.Ganadera.Business.Domain.Base
{
    /// <summary>
    /// Extiende la entidad base con datos de auditoria para entidades que registran creacion y modificacion.
    /// </summary>
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime Fecha_Creado { get; set; }
        public DateTime? Fecha_Modificado { get; set; }
        public long Creado_Por { get; set; }
        public long? Modificado_Por { get; set; }
    }
}
