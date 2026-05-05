namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Interfaces;

using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Models;

public interface IIdentificadorService
{
    Task<long> ObtenerTipoIdentificadorInternoCodigoAsync(long fincaCodigo, CancellationToken cancellationToken = default);
    string ConstruirIdentificadorInterno(long animalCodigo);
    Task<int> ObtenerSiguienteConsecutivoAsync(long fincaCodigo, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExistenciaIdentificador>> VerificarExistenciaIdentificadoresAsync(long fincaCodigo, IEnumerable<string> identificadores, CancellationToken cancellationToken = default);
}
