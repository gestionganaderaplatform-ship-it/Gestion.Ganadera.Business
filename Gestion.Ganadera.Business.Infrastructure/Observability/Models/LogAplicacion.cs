namespace Gestion.Ganadera.Business.Infrastructure.Observability.Models
{
    /// <summary>
/// Representa una entrada de log persistida para soporte y observabilidad operativa.
/// </summary>
public class LogAplicacion
    {
        public long Log_Aplicacion_Codigo { get; set; }

        public string Log_Aplicacion_Api_Codigo { get; set; } = string.Empty;
        public string Log_Aplicacion_Nivel { get; set; } = string.Empty;
        public string Log_Aplicacion_Mensaje { get; set; } = string.Empty;
        public string? Log_Aplicacion_Excepcion { get; set; }

        public string Log_Aplicacion_Origen { get; set; } = string.Empty;   // API / Service / Repository
        public string? Log_Aplicacion_Metodo { get; set; }
        public string? Log_Aplicacion_Ruta { get; set; }

        public long? Cliente_Codigo { get; set; }
        public string? Log_Aplicacion_Usuario { get; set; }
        public string Log_Aplicacion_CorrelationId { get; set; } = string.Empty;

        public DateTime Log_Aplicacion_Fecha { get; set; }
    }
}

