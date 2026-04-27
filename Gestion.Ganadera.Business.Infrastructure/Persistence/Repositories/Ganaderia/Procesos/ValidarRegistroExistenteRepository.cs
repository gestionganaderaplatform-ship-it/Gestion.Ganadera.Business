using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class ValidarRegistroExistenteRepository(AppDbContext context) : IValidarRegistroExistenteRepository
{
    public async Task<bool> ExisteIdentificadorActivoEnFincaAsync(
        long fincaCodigo,
        string identificadorPrincipal,
        CancellationToken cancellationToken = default)
    {
        return await context.IdentificadoresAnimal
            .AsNoTracking()
            .Join(
                context.Animales.AsNoTracking(),
                identificador => identificador.Animal_Codigo,
                animal => animal.Animal_Codigo,
                (identificador, animal) => new { identificador, animal })
            .AnyAsync(
                item => item.identificador.Identificador_Animal_Valor == identificadorPrincipal
                        && item.identificador.Identificador_Animal_Activo
                        && item.animal.Finca_Codigo == fincaCodigo,
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
