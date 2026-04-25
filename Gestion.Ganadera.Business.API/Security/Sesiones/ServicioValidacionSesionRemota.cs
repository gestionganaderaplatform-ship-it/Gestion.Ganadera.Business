using System.Net;
using System.Net.Http.Headers;
using Gestion.Ganadera.Business.API.Options;
using Microsoft.Extensions.Options;

namespace Gestion.Ganadera.Business.API.Security.Sesiones
{
    public sealed class ServicioValidacionSesionRemota(
        HttpClient httpClient,
        IOptions<OpcionesApiAuth> opciones) : IServicioValidacionSesionRemota
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly OpcionesApiAuth _opciones = opciones.Value;

        public async Task<ResultadoValidacionSesionRemota> ValidarAsync(
            string authorizationHeader,
            string? correlationId,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(_opciones.BaseUrl))
            {
                return ResultadoValidacionSesionRemota.SinConfigurar;
            }

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                _opciones.ValidarSesionActualPath);

            if (AuthenticationHeaderValue.TryParse(authorizationHeader, out var authorization))
            {
                request.Headers.Authorization = authorization;
            }
            else
            {
                request.Headers.TryAddWithoutValidation("Authorization", authorizationHeader);
            }

            if (!string.IsNullOrWhiteSpace(correlationId))
            {
                request.Headers.TryAddWithoutValidation("X-Correlation-Id", correlationId);
            }

            try
            {
                using var response = await _httpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return ResultadoValidacionSesionRemota.Activa;
                }

                return response.StatusCode switch
                {
                    HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden
                        => ResultadoValidacionSesionRemota.Invalida,
                    _ => ResultadoValidacionSesionRemota.NoDisponible
                };
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                return ResultadoValidacionSesionRemota.NoDisponible;
            }
            catch (HttpRequestException)
            {
                return ResultadoValidacionSesionRemota.NoDisponible;
            }
        }
    }
}
