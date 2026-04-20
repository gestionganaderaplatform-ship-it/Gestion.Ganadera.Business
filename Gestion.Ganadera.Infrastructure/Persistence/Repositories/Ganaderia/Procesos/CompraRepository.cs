using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.Compra.Interfaces;
using Gestion.Ganadera.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class CompraRepository(AppDbContext context) : ICompraRepository
{
    public async Task<bool> CrearRegistroAtómicoAsync(
        Animal animal,
        IdentificadorAnimal identificador,
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleCompra fotoRegistro,
        CancellationToken cancellationToken = default)
    {
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        await context.Animales.AddAsync(animal, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        identificador.Animal_Codigo = animal.Animal_Codigo;
        await context.IdentificadoresAnimal.AddAsync(identificador, cancellationToken);

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
    }
}
