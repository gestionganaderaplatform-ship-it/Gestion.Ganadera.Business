using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class NacimientoRepository(AppDbContext context) : INacimientoRepository
{
    private const string TipoIdentificadorInternoSistema = "INTERNO_SISTEMA";

    public async Task<bool> RegistrarAtomicoAsync(
        Animal cria,
        IdentificadorAnimal identificador,
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleNacimiento detalle,
        AnimalRelacionFamiliar relacion,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await context.Animales.AddAsync(cria, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                var tipoIdentificadorInternoCodigo = await ObtenerTipoIdentificadorInternoCodigoAsync(
                    cria.Finca_Codigo,
                    cancellationToken);

                var identificadorInterno = new IdentificadorAnimal
                {
                    Animal_Codigo = cria.Animal_Codigo,
                    Tipo_Identificador_Codigo = tipoIdentificadorInternoCodigo,
                    Identificador_Animal_Valor = ConstruirIdentificadorInterno(cria.Animal_Codigo),
                    Identificador_Animal_Es_Principal = false,
                    Identificador_Animal_Activo = true
                };

                identificador.Animal_Codigo = cria.Animal_Codigo;
                await context.IdentificadoresAnimal.AddRangeAsync(
                    [identificador, identificadorInterno],
                    cancellationToken);

                await context.EventosGanaderos.AddAsync(evento, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                eventoAnimal.Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo;
                eventoAnimal.Animal_Codigo = cria.Animal_Codigo;
                await context.EventosGanaderosAnimal.AddAsync(eventoAnimal, cancellationToken);

                relacion.Animal_Codigo_Cria = cria.Animal_Codigo;
                await context.AnimalesRelacionesFamiliares.AddAsync(relacion, cancellationToken);

                detalle.Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo;
                detalle.Animal_Codigo_Cria = cria.Animal_Codigo;
                await context.EventosDetalleNacimiento.AddAsync(detalle, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    public Task<bool> MadreExisteEnFincaAsync(
        long fincaCodigo,
        long madreAnimalCodigo,
        CancellationToken cancellationToken = default)
    {
        return context.Animales
            .AsNoTracking()
            .AnyAsync(
                animal =>
                    animal.Finca_Codigo == fincaCodigo &&
                    animal.Animal_Codigo == madreAnimalCodigo &&
                    animal.Animal_Activo,
                cancellationToken);
    }

    public Task<bool> MadreElegibleEnFincaAsync(
        long fincaCodigo,
        long madreAnimalCodigo,
        CancellationToken cancellationToken = default)
    {
        return (from animal in context.Animales.AsNoTracking()
                join categoria in context.CategoriasAnimal.AsNoTracking()
                    on animal.Categoria_Animal_Codigo equals categoria.Categoria_Animal_Codigo
                where animal.Finca_Codigo == fincaCodigo
                      && animal.Animal_Codigo == madreAnimalCodigo
                      && animal.Animal_Activo
                      && animal.Animal_Sexo.ToUpper() == "HEMBRA"
                      && (
                          categoria.Categoria_Animal_Nombre.ToUpper().Contains("VACA")
                          || categoria.Categoria_Animal_Nombre.ToUpper().Contains("NOVILLA"))
                select animal.Animal_Codigo)
            .AnyAsync(cancellationToken);
    }

    public async Task<int> ObtenerSiguienteConsecutivoAsync(
        long fincaCodigo,
        CancellationToken cancellationToken = default)
    {
        var totalAnimales = await context.Animales
            .AsNoTracking()
            .CountAsync(item => item.Finca_Codigo == fincaCodigo, cancellationToken);

        return totalAnimales + 1;
    }

    private async Task<long> ObtenerTipoIdentificadorInternoCodigoAsync(
        long fincaCodigo,
        CancellationToken cancellationToken)
    {
        var clienteCodigo = await context.Fincas
            .Where(f => f.Finca_Codigo == fincaCodigo)
            .Select(f => f.Cliente_Codigo)
            .FirstOrDefaultAsync(cancellationToken);

        var tipoIdentificadorInternoCodigo = await context.TiposIdentificador
            .IgnoreQueryFilters()
            .Where(item =>
                item.Cliente_Codigo == clienteCodigo &&
                item.Tipo_Identificador_Codigo_Interno == TipoIdentificadorInternoSistema &&
                item.Tipo_Identificador_Activo)
            .Select(item => (long?)item.Tipo_Identificador_Codigo)
            .FirstOrDefaultAsync(cancellationToken);

        if (!tipoIdentificadorInternoCodigo.HasValue)
        {
            throw new ValidationException(
            [
                new ValidationFailure(
                    nameof(IdentificadorAnimal.Tipo_Identificador_Codigo),
                    NacimientoMessages.TipoIdentificadorInternoNoDisponible)
            ]);
        }

        return tipoIdentificadorInternoCodigo.Value;
    }

    private static string ConstruirIdentificadorInterno(long animalCodigo)
        => $"INT-{animalCodigo:D10}";
}
