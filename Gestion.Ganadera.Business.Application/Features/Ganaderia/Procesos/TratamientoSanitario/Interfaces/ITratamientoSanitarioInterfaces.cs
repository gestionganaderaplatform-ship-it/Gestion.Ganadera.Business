using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Interfaces;

public interface ITratamientoSanitarioRepository
{
    Task<bool> RegistrarAtomicoAsync(
        IEnumerable<EventoGanadero> eventos,
        IEnumerable<EventoGanaderoAnimal> eventosAnimal,
        IEnumerable<EventoDetalleTratamientoSanitario> detalles,
        CancellationToken cancellationToken = default);
}

public interface ITratamientoSanitarioService
{
    Task<bool> RegistrarAsync(RegistrarTratamientoRequest request, CancellationToken cancellationToken = default);
    Task<bool> RegistrarLoteAsync(RegistrarTratamientoLoteRequest request, CancellationToken cancellationToken = default);
}
