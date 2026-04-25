namespace Gestion.Ganadera.Business.Application.Observability.ViewModels
{
    /// <summary>
    /// Describe una metrica tecnica de request lista para ser persistida por infraestructura.
    /// </summary>
    public class MetricaSolicitudViewModel
    {
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
