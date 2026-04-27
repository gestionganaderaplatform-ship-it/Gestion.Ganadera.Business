namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;

public interface IValidarRegistroExistenteRepository
{
    Task<bool> ExisteIdentificadorActivoEnFincaAsync(
        long fincaCodigo,
        string identificadorPrincipal,
        CancellationToken cancellationToken = default);

    Task<bool> FincaPoseePotreroAsync(long fincaCodigo, long potreroCodigo, CancellationToken cancellationToken = default);
}
