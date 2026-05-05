using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class MuerteRepository(
    AppDbContext context,
    ICurrentActorProvider currentActorProvider) : IMuerteRepository
{
    public async Task<bool> RegistrarAtomicoAsync(
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleMuerte detalle,
        Animal animalActualizado,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var animalCodigo = animalActualizado.Animal_Codigo;

            var animal = await context.Animales
                .FirstOrDefaultAsync(a => a.Animal_Codigo == animalCodigo, cancellationToken);

            if (animal is null)
            {
                throw new ValidationException(
                [
                    new ValidationFailure(nameof(Animal.Animal_Codigo), MuerteMessages.AnimalNoEncontrado)
                ]);
            }

            if (!animal.Animal_Activo)
            {
                throw new ValidationException(
                [
                    new ValidationFailure(nameof(Animal.Animal_Codigo), MuerteMessages.AnimalInactivo)
                ]);
            }

            var yaMuerto = await context.EventosGanaderos
                .Join(context.EventosGanaderosAnimal,
                    e => e.Evento_Ganadero_Codigo,
                    ea => ea.Evento_Ganadero_Codigo,
                    (e, ea) => new { e, ea })
                .AnyAsync(x => x.ea.Animal_Codigo == animalCodigo
                    && x.e.Evento_Ganadero_Tipo == EventoGanaderoTipo.Muerte
                    && x.e.Evento_Ganadero_Estado == EventoGanaderoEstado.Completado,
                    cancellationToken);

            if (yaMuerto)
            {
                throw new ValidationException(
                [
                    new ValidationFailure(nameof(Animal.Animal_Codigo), MuerteMessages.AnimalYaMuerto)
                ]);
            }

            if (evento.Finca_Codigo == 0)
                evento.Finca_Codigo = animal.Finca_Codigo;
            evento.Cliente_Codigo = animal.Cliente_Codigo;
            eventoAnimal.Cliente_Codigo = animal.Cliente_Codigo;
            detalle.Cliente_Codigo = animal.Cliente_Codigo;

            await context.EventosGanaderos.AddAsync(evento, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            eventoAnimal.Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo;
            await context.EventosGanaderosAnimal.AddAsync(eventoAnimal, cancellationToken);

            detalle.Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo;
            await context.EventosDetalleMuerte.AddAsync(detalle, cancellationToken);

            await context.Animales
                .Where(a => a.Animal_Codigo == animalCodigo)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(a => a.Animal_Activo, false)
                    .SetProperty(a => a.Animal_Fecha_Ultimo_Evento, detalle.Evento_Detalle_Muerte_Fecha)
                    .SetProperty(a => a.Animal_Fecha_Muerte, detalle.Evento_Detalle_Muerte_Fecha)
                    .SetProperty(a => a.Causa_Muerte_Codigo, detalle.Causa_Muerte_Codigo)
                    .SetProperty(a => a.Fecha_Modificado, DateTime.Now)
                    .SetProperty(a => a.Modificado_Por, currentActorProvider.ActorNumericId),
                    cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        });
    }
}