namespace Gestion.Ganadera.Application.Observability.ViewModels
{
    /// <summary>
    /// Describe un evento tecnico de seguridad listo para ser registrado por infraestructura.
    /// </summary>
    public class EventoSeguridadViewModel
    {
        public long Evento_Seguridad_Codigo { get; set; }
        public DateTime Evento_Seguridad_Fecha { get; set; }
        public string Evento_Seguridad_Tipo_Evento { get; set; } = null!;
        public string Evento_Seguridad_Ip { get; set; } = null!;
        public string Evento_Seguridad_Endpoint { get; set; } = null!;
        public string? Evento_Seguridad_Origin { get; set; }
        public string? Evento_Seguridad_UserAgent { get; set; }
        public string? Evento_Seguridad_CorrelationId { get; set; }
    }
}
