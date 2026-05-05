using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Interfaces;

public interface IVacunacionService
{
    Task<bool> RegistrarAsync(RegistrarVacunacionRequest request, CancellationToken cancellationToken = default);
    Task<bool> ValidarAsync(ValidarVacunacionRequest request, CancellationToken cancellationToken = default);
    Task<bool> RegistrarLoteAsync(RegistrarVacunacionLoteRequest request, CancellationToken cancellationToken = default);
}