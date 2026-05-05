using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class CompraRepository(AppDbContext context, IIdentificadorService identificadorService) : ICompraRepository
{
    public async Task<bool> CrearRegistroAtomicoAsync(
        Animal animal,
        IdentificadorAnimal identificador,
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleCompra fotoRegistro,
        CancellationToken cancellationToken = default)
    {
        return await RegistrarLoteAtomicoAsync(
            [(animal, identificador, evento, eventoAnimal, fotoRegistro)],
            cancellationToken);
    }

    public async Task<bool> RegistrarLoteAtomicoAsync(
        IEnumerable<(Animal Animal, IdentificadorAnimal Identificador, EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleCompra Foto)> lote,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            var tipoIdentificadorInternoCache = new Dictionary<long, long>();

            foreach (var item in lote)
            {
                await context.Animales.AddAsync(item.Animal, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                if (!tipoIdentificadorInternoCache.TryGetValue(item.Animal.Finca_Codigo, out var tipoIdentificadorInternoCodigo))
                {
                    tipoIdentificadorInternoCodigo = await identificadorService.ObtenerTipoIdentificadorInternoCodigoAsync(
                        item.Animal.Finca_Codigo,
                        cancellationToken);
                    tipoIdentificadorInternoCache[item.Animal.Finca_Codigo] = tipoIdentificadorInternoCodigo;
                }

                var identificadorInterno = new IdentificadorAnimal
                {
                    Animal_Codigo = item.Animal.Animal_Codigo,
                    Tipo_Identificador_Codigo = tipoIdentificadorInternoCodigo,
                    Identificador_Animal_Valor = identificadorService.ConstruirIdentificadorInterno(item.Animal.Animal_Codigo),
                    Identificador_Animal_Es_Principal = false,
                    Identificador_Animal_Activo = true
                };

                item.Identificador.Animal_Codigo = item.Animal.Animal_Codigo;
                await context.IdentificadoresAnimal.AddRangeAsync(
                    [item.Identificador, identificadorInterno],
                    cancellationToken);

                await context.EventosGanaderos.AddAsync(item.Evento, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                item.EventoAnimal.Evento_Ganadero_Codigo = item.Evento.Evento_Ganadero_Codigo;
                item.EventoAnimal.Animal_Codigo = item.Animal.Animal_Codigo;
                await context.EventosGanaderosAnimal.AddAsync(item.EventoAnimal, cancellationToken);

                item.Foto.Evento_Ganadero_Codigo = item.Evento.Evento_Ganadero_Codigo;
                await context.EventosDetalleCompra.AddAsync(item.Foto, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        });
    }

    public async Task<int> ObtenerSiguienteConsecutivoAsync(long fincaCodigo, CancellationToken cancellationToken = default)
        => await identificadorService.ObtenerSiguienteConsecutivoAsync(fincaCodigo, cancellationToken);
}

