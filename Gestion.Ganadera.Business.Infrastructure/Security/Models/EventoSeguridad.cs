namespace Gestion.Ganadera.Business.Infrastructure.Security.Models
{
    /// <summary>
/// Modelo persistido de eventos tecnicos de seguridad generados por el API.
/// </summary>
public class EventoSeguridad
    {
        public long Evento_Seguridad_Codigo { get; set; }
        public string Evento_Seguridad_Api_Codigo { get; set; } = string.Empty;
        public DateTime Evento_Seguridad_Fecha { get; set; }
        public string Evento_Seguridad_Tipo_Evento { get; set; } = null!;
        public long? Cliente_Codigo { get; set; }
        public string Evento_Seguridad_Ip { get; set; } = null!;
        public string Evento_Seguridad_Endpoint { get; set; } = null!;
        public string? Evento_Seguridad_Origin { get; set; }
        public string? Evento_Seguridad_UserAgent { get; set; }
        public string? Evento_Seguridad_CorrelationId { get; set; }
    }
}

