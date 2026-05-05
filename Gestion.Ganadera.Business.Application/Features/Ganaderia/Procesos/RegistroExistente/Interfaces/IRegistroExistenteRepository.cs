using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;

public interface IRegistroExistenteRepository : IValidarRegistroExistenteRepository
{
    Task<bool> RegistrarAtomicoAsync(
        Animal animal,
        IdentificadorAnimal identificador,
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleRegistroExistente fotoRegistro,
        CancellationToken cancellationToken = default);

    Task<bool> RegistrarLoteAtomicoAsync(
        IEnumerable<(Animal Animal, IdentificadorAnimal Identificador, EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleRegistroExistente Foto)> lote,
        CancellationToken cancellationToken = default);

    Task<int> ObtenerSiguienteConsecutivoAsync(long fincaCodigo, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExistenciaIdentificador>> ExistentesIdentificadoresAsync(long fincaCodigo, IEnumerable<string> identificadores, CancellationToken cancellationToken = default);
}