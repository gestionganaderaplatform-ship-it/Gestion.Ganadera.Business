using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class CambioCategoriaRepository(AppDbContext context) : ICambioCategoriaRepository
{
    public async Task<IEnumerable<Animal>> ObtenerCandidatosCambioAsync(long fincaCodigo, CancellationToken cancellationToken = default)
    {
        return await context.Animales
            .Where(x => x.Animal_Activo && x.Finca_Codigo == fincaCodigo)
            .Include(x => x.Categoria)
            .ToListAsync(cancellationToken);
    }

    public async Task<CambioCategoriaSugerido?> ObtenerSugerenciaPorCodigoAsync(long codigo, CancellationToken cancellationToken = default)
    {
        return await context.CambiosCategoriaSugeridos
            .Include(x => x.Animal)
            .FirstOrDefaultAsync(x => x.Cambio_Categoria_Sugerido_Codigo == codigo, cancellationToken);
    }

    public async Task<IEnumerable<CategoriaAnimal>> ObtenerCategoriasConReglasAsync(CancellationToken cancellationToken = default)
    {
        return await context.CategoriasAnimal
            .Where(x => x.Categoria_Animal_Activa && x.Categoria_Animal_Siguiente_Codigo != null)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> RegistrarSugerenciasMasivamenteAsync(IEnumerable<CambioCategoriaSugerido> sugerencias, CancellationToken cancellationToken = default)
    {
        await context.CambiosCategoriaSugeridos.AddRangeAsync(sugerencias, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<SugerenciaCambioViewModel>> ObtenerSugerenciasPendientesDetalladasAsync(long fincaCodigo, CancellationToken cancellationToken = default)
    {
        return await context.CambiosCategoriaSugeridos
            .Where(x => x.Sugerencia_Estado == CambioCategoriaSugerenciaEstado.Pendiente && x.Animal!.Finca_Codigo == fincaCodigo)
            .Include(x => x.Animal)
            .Include(x => x.CategoriaActual)
            .Include(x => x.CategoriaSugerida)
            .Select(x => new SugerenciaCambioViewModel
            {
                Sugerencia_Codigo = x.Cambio_Categoria_Sugerido_Codigo,
                Animal_Codigo = x.Animal_Codigo,
                Animal_Identificador = context.IdentificadoresAnimal
                    .Where(i => i.Animal_Codigo == x.Animal_Codigo && i.Identificador_Animal_Es_Principal)
                    .Select(i => i.Identificador_Animal_Valor)
                    .FirstOrDefault() ?? "SIN ID",
                Categoria_Actual_Codigo = x.Categoria_Actual_Codigo,
                Categoria_Actual_Nombre = x.CategoriaActual!.Categoria_Animal_Nombre,
                Categoria_Sugerida_Codigo = x.Categoria_Sugerida_Codigo,
                Categoria_Sugerida_Nombre = x.CategoriaSugerida!.Categoria_Animal_Nombre,
                Motivo = x.Sugerencia_Motivo,
                Edad_Meses_Aproximada = x.Animal!.Animal_Fecha_Nacimiento != null 
                    ? (int)((DateTime.Now - x.Animal.Animal_Fecha_Nacimiento.Value).TotalDays / 30.44)
                    : (int)((DateTime.Now - x.Animal.Animal_Fecha_Ingreso_Inicial).TotalDays / 30.44),
                Ultimo_Peso = x.Animal.Animal_Peso,
                Fecha_Sugerencia = x.Fecha_Sugerencia
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AplicarCambioCategoriaAtomicoAsync(
        Animal animal,
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleCambioCategoria detalle,
        CambioCategoriaSugerido sugerencia,
        CancellationToken cancellationToken = default)
    {
        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            // 1. Actualizar Animal
            context.Animales.Update(animal);

            // 2. Crear Evento
            await context.EventosGanaderos.AddAsync(evento, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            // 3. Vincular Animal al Evento
            eventoAnimal.Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo;
            eventoAnimal.Animal_Codigo = animal.Animal_Codigo;
            await context.EventosGanaderosAnimal.AddAsync(eventoAnimal, cancellationToken);

            // 4. Crear Detalle del Evento
            detalle.Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo;
            await context.EventosDetalleCambioCategoria.AddAsync(detalle, cancellationToken);

            // 5. Cerrar Sugerencia
            sugerencia.Sugerencia_Estado = CambioCategoriaSugerenciaEstado.Aprobado;
            context.CambiosCategoriaSugeridos.Update(sugerencia);

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return true;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return false;
        }
    }

    public async Task<bool> RechazarSugerenciasAsync(IEnumerable<long> sugerenciasCodigos, CancellationToken cancellationToken = default)
    {
        var sugerencias = await context.CambiosCategoriaSugeridos
            .Where(x => sugerenciasCodigos.Contains(x.Cambio_Categoria_Sugerido_Codigo))
            .ToListAsync(cancellationToken);

        foreach (var sug in sugerencias)
        {
            sug.Sugerencia_Estado = CambioCategoriaSugerenciaEstado.Rechazado;
        }

        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
