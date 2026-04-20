using Gestion.Ganadera.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;

public interface IRegistroExistenteRepository
{
    Task<bool> CrearRegistroAtómicoAsync(
        Animal animal, 
        IdentificadorAnimal identificador, 
        EventoGanadero evento, 
        EventoGanaderoAnimal eventoAnimal, 
        EventoDetalleRegistroExistente fotoRegistro, 
        CancellationToken cancellationToken = default);
}
