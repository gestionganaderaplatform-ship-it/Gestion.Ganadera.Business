using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Interfaces;

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
