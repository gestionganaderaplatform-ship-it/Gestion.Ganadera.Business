using System.ComponentModel.DataAnnotations;

namespace Gestion.Ganadera.Business.Infrastructure.Observability.Models
{

    /// <summary>
/// Almacena metricas basicas de requests para analisis operativo y monitoreo.
/// </summary>
public class MetricaSolicitud
    {
        [Key]
        public long Metrica_Solicitud_Codigo { get; set; }

        public string Metrica_Solicitud_Api_Codigo { get; set; } = string.Empty;
        public long? Cliente_Codigo { get; set; }
        public string Metrica_Solicitud_Metodo_Http { get; set; } = string.Empty;
        public string Metrica_Solicitud_Ruta_Request { get; set; } = string.Empty;
        public int Metrica_Solicitud_Codigo_Estado { get; set; }
        public long Metrica_Solicitud_Tiempo_Respuesta_Ms { get; set; }
        public string? Metrica_Solicitud_Correlation_Id { get; set; }
        public DateTime Metrica_Solicitud_Fecha_Creacion { get; set; }
    }
}

