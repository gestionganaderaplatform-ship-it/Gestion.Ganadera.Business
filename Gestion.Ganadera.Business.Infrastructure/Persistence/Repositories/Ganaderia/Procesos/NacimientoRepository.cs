using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class NacimientoRepository(AppDbContext context, IIdentificadorService identificadorService) : INacimientoRepository
{
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
            await context.Animales.AddAsync(cria, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var tipoIdentificadorInternoCodigo = await identificadorService.ObtenerTipoIdentificadorInternoCodigoAsync(
                cria.Finca_Codigo,
                cancellationToken);

            var identificadorInterno = new IdentificadorAnimal
            {
                Animal_Codigo = cria.Animal_Codigo,
                Tipo_Identificador_Codigo = tipoIdentificadorInternoCodigo,
                Identificador_Animal_Valor = identificadorService.ConstruirIdentificadorInterno(cria.Animal_Codigo),
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
        => await identificadorService.ObtenerSiguienteConsecutivoAsync(fincaCodigo, cancellationToken);
}

