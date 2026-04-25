using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class CompraRepository(AppDbContext context) : ICompraRepository
{
    private const string TipoIdentificadorInternoSistema = "INTERNO_SISTEMA";

    public async Task<bool> CrearRegistroAtómicoAsync(
        Animal animal,
        IdentificadorAnimal identificador,
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleCompra fotoRegistro,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            await context.Animales.AddAsync(animal, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var tipoIdentificadorInternoCodigo = await ObtenerTipoIdentificadorInternoCodigoAsync(
                animal.Cliente_Codigo,
                cancellationToken);

            var identificadorInterno = new IdentificadorAnimal
            {
                Animal_Codigo = animal.Animal_Codigo,
                Tipo_Identificador_Codigo = tipoIdentificadorInternoCodigo,
                Identificador_Animal_Valor = ConstruirIdentificadorInterno(animal.Animal_Codigo),
                Identificador_Animal_Es_Principal = false,
                Identificador_Animal_Activo = true
            };

            identificador.Animal_Codigo = animal.Animal_Codigo;
            await context.IdentificadoresAnimal.AddRangeAsync(
                [identificador, identificadorInterno],
                cancellationToken);

            await context.EventosGanaderos.AddAsync(evento, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            eventoAnimal.Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo;
            eventoAnimal.Animal_Codigo = animal.Animal_Codigo;
            await context.EventosGanaderosAnimal.AddAsync(eventoAnimal, cancellationToken);

            fotoRegistro.Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo;
            await context.EventosDetalleCompra.AddAsync(fotoRegistro, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        });
    }

    private async Task<long> ObtenerTipoIdentificadorInternoCodigoAsync(
        long? clienteCodigo,
        CancellationToken cancellationToken)
    {
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
                    CompraMessages.TipoIdentificadorInternoNoDisponible)
            ]);
        }

        return tipoIdentificadorInternoCodigo.Value;
    }

    private static string ConstruirIdentificadorInterno(long animalCodigo)
        => $"INT-{animalCodigo:D10}";
}
