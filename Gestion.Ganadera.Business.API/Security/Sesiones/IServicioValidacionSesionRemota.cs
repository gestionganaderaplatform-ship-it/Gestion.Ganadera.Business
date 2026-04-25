namespace Gestion.Ganadera.Business.API.Security.Sesiones
{
    public interface IServicioValidacionSesionRemota
    {
        Task<ResultadoValidacionSesionRemota> ValidarAsync(
            string authorizationHeader,
            string? correlationId,
            CancellationToken cancellationToken = default);
    }
}
