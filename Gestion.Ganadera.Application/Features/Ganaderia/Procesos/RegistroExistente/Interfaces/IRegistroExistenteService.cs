using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;

public interface IRegistroExistenteService
{
    Task<bool> CrearRegistroAsync(RegistrarExistenteRequest request, CancellationToken cancellationToken = default);
}
