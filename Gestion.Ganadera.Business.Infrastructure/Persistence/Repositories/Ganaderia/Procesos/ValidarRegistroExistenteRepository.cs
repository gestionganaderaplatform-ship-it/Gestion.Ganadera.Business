using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class ValidarRegistroExistenteRepository(AppDbContext context) : IValidarRegistroExistenteRepository
{
    public async Task<bool> ExisteIdentificadorActivoEnClienteAsync(
        long fincaCodigo,
        string identificadorPrincipal,
        long tipoIdentificadorCodigo,
        CancellationToken cancellationToken = default)
    {
        var clienteCodigo = await context.Fincas
            .AsNoTracking()
            .Where(f => f.Finca_Codigo == fincaCodigo)
            .Select(f => f.Cliente_Codigo)
            .FirstOrDefaultAsync(cancellationToken);

        return await context.IdentificadoresAnimal
            .AsNoTracking()
            .Join(
                context.Animales.AsNoTracking(),
                identificador => identificador.Animal_Codigo,
                animal => animal.Animal_Codigo,
                (identificador, animal) => new { identificador, animal })
            .AnyAsync(
                item => item.identificador.Identificador_Animal_Valor == identificadorPrincipal
                        && item.identificador.Tipo_Identificador_Codigo == tipoIdentificadorCodigo
                        && item.identificador.Identificador_Animal_Activo
                        && item.animal.Cliente_Codigo == clienteCodigo,
                cancellationToken);
    }

    public async Task<bool> FincaPoseePotreroAsync(
        long fincaCodigo, 
        long potreroCodigo, 
        CancellationToken cancellationToken = default)
    {
        return await context.Potreros.AsNoTracking()
            .AnyAsync(x => x.Finca_Codigo == fincaCodigo 
                        && x.Potrero_Codigo == potreroCodigo, cancellationToken);
    }
}
