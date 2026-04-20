using Gestion.Ganadera.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Procesos.Compra.Interfaces;

public interface ICompraRepository
{
    Task<bool> CrearRegistroAtómicoAsync(
        Animal animal,
        IdentificadorAnimal identificador,
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleCompra fotoRegistro,
        CancellationToken cancellationToken = default);
}
