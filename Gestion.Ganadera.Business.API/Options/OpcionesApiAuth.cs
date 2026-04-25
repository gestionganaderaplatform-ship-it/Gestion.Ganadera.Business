namespace Gestion.Ganadera.Business.API.Options
{
    public sealed class OpcionesApiAuth
    {
        public const string SectionName = "AuthApi";

        public string? BaseUrl { get; set; }
        public string ValidarSesionActualPath { get; set; } =
            "/api/v1/seguridad/autenticacion/sesiones/actual/estado";
        public int TimeoutSeconds { get; set; } = 5;
    }
}
