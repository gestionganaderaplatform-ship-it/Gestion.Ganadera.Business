using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Interfaces;

public interface INacimientoService
{
    Task<bool> RegistrarAsync(
        RegistrarNacimientoRequest request,
        CancellationToken cancellationToken = default);

    Task<int> ObtenerSiguienteConsecutivoAsync(
        long fincaCodigo,
        CancellationToken cancellationToken = default);
}
