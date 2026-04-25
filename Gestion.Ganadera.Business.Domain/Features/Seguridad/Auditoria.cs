namespace Gestion.Ganadera.Business.Domain.Features.Seguridad
{
    /// <summary>
    /// Registra cambios realizados sobre entidades auditables para trazabilidad funcional del sistema.
    /// </summary>
    public class Auditoria
    {
        public long Auditoria_Codigo { get; set; }
        public long? Cliente_Codigo { get; set; }
        public string Auditoria_Api_Codigo { get; set; } = string.Empty;
        public string Auditoria_Nombre_Tabla { get; set; } = string.Empty;
        public string Auditoria_Valor_Clave { get; set; } = string.Empty;
        public string Auditoria_Valores_Viejos { get; set; } = string.Empty;
        public string Auditoria_Nuevos_Valores { get; set; } = string.Empty;
        public string Auditoria_Modificado_Por { get; set; } = string.Empty;
        public DateTime Auditoria_Fecha_Modificado { get; set; }
    }
}
