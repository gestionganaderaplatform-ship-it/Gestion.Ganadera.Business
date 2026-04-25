namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;

public interface IValidarRegistroExistenteRepository
{
    Task<bool> ExisteIdentificadorActivoEnClienteAsync(
        long fincaCodigo,
        string identificadorPrincipal,
        long tipoIdentificadorCodigo,
        CancellationToken cancellationToken = default);
    Task<bool> FincaPoseePotreroAsync(long fincaCodigo, long potreroCodigo, CancellationToken cancellationToken = default);
}
