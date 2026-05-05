using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Interfaces;

public interface ITrasladoFincaService
{
    Task<bool> RegistrarAsync(RegistrarTrasladoFincaRequest request, CancellationToken cancellationToken = default);
    Task<bool> ValidarAsync(ValidarTrasladoFincaRequest request, CancellationToken cancellationToken = default);
    Task<bool> RegistrarLoteAsync(RegistrarTrasladoFincaLoteRequest request, CancellationToken cancellationToken = default);
}
