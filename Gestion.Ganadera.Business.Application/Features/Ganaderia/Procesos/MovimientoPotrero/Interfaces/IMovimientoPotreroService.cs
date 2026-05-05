using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Interfaces;

public interface IMovimientoPotreroService
{
    Task<bool> RegistrarAsync(RegistrarMovimientoPotreroRequest request, CancellationToken cancellationToken = default);
    Task<bool> ValidarAsync(ValidarMovimientoPotreroRequest request, CancellationToken cancellationToken = default);
    Task<bool> RegistrarLoteAsync(RegistrarMovimientoPotreroLoteRequest request, CancellationToken cancellationToken = default);
}