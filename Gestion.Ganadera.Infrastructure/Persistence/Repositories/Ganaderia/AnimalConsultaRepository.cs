using Gestion.Ganadera.Application.Features.Ganaderia.Animales.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Animales.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Infrastructure.Persistence.Repositories.Ganaderia;

public class AnimalConsultaRepository(AppDbContext context) : IAnimalConsultaRepository
{
    public async Task<(IEnumerable<GanadoViewModel> Items, int TotalRegistros)> ObtenerPorPaginado(
        int pagina, 
        int tamañoPagina, 
        CancellationToken cancellationToken = default)
    {
        var query = from animal in context.Animales.AsNoTracking()
                    join finca in context.Fincas.AsNoTracking() on animal.Finca_Codigo equals finca.Finca_Codigo
                    join potrero in context.Potreros.AsNoTracking() on animal.Potrero_Codigo equals potrero.Potrero_Codigo
                    join categoria in context.CategoriasAnimal.AsNoTracking() on animal.Categoria_Animal_Codigo equals categoria.Categoria_Animal_Codigo
                    from principalIdent in context.IdentificadoresAnimal.AsNoTracking()
                        .Where(i => i.Animal_Codigo == animal.Animal_Codigo 
                                 && i.Identificador_Animal_Es_Principal 
                                 && i.Identificador_Animal_Activo)
                        .DefaultIfEmpty()
                    select new GanadoViewModel
                    {
                        Animal_Codigo = animal.Animal_Codigo,
                        Animal_Identificador_Principal = principalIdent != null ? principalIdent.Identificador_Animal_Valor : "Sin identificador",
                        Animal_Sexo = animal.Animal_Sexo,
                        Categoria_Animal_Nombre = categoria.Categoria_Animal_Nombre,
                        Finca_Nombre = finca.Finca_Nombre,
                        Potrero_Nombre = potrero.Potrero_Nombre,
                        Animal_Activo = animal.Animal_Activo
                    };

        var totalRegistros = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.Animal_Codigo)
            .Skip((pagina - 1) * tamañoPagina)
            .Take(tamañoPagina)
            .ToListAsync(cancellationToken);

        return (items, totalRegistros);
    }

    public async Task<AnimalViewModel?> Consultar(
        long animalCodigo, 
        CancellationToken cancellationToken = default)
    {
        var query = from animal in context.Animales.AsNoTracking()
                    where animal.Animal_Codigo == animalCodigo
                    join finca in context.Fincas.AsNoTracking() on animal.Finca_Codigo equals finca.Finca_Codigo
                    join potrero in context.Potreros.AsNoTracking() on animal.Potrero_Codigo equals potrero.Potrero_Codigo
                    join categoria in context.CategoriasAnimal.AsNoTracking() on animal.Categoria_Animal_Codigo equals categoria.Categoria_Animal_Codigo
                    from principalIdent in context.IdentificadoresAnimal.AsNoTracking()
                        .Where(i => i.Animal_Codigo == animal.Animal_Codigo 
                                 && i.Identificador_Animal_Es_Principal 
                                 && i.Identificador_Animal_Activo)
                        .DefaultIfEmpty()
                    select new AnimalViewModel
                    {
                        Animal_Codigo = animal.Animal_Codigo,
                        Animal_Identificador_Principal = principalIdent != null ? principalIdent.Identificador_Animal_Valor : "Sin identificador",
                        Animal_Sexo = animal.Animal_Sexo,
                        Categoria_Animal_Nombre = categoria.Categoria_Animal_Nombre,
                        Finca_Nombre = finca.Finca_Nombre,
                        Potrero_Nombre = potrero.Potrero_Nombre,
                        Animal_Activo = animal.Animal_Activo,
                        Animal_Origen_Ingreso = animal.Animal_Origen_Ingreso,
                        Animal_Fecha_Ingreso_Inicial = animal.Animal_Fecha_Ingreso_Inicial,
                        Animal_Fecha_Registro_Ingreso = animal.Animal_Fecha_Registro_Ingreso,
                        Animal_Fecha_Ultimo_Evento = animal.Animal_Fecha_Ultimo_Evento
                    };

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<AnimalHistorialViewModel>> ObtenerHistorialAsync(long animalCodigo, CancellationToken cancellationToken = default)
    {
        var eventos = await (from ea in context.EventosGanaderosAnimal.AsNoTracking()
                             join e in context.EventosGanaderos.AsNoTracking() 
                               on ea.Evento_Ganadero_Codigo equals e.Evento_Ganadero_Codigo
                             where ea.Animal_Codigo == animalCodigo
                             orderby e.Evento_Ganadero_Fecha descending
                             select new AnimalHistorialViewModel
                             {
                                 Evento_Ganadero_Animal_Codigo = ea.Evento_Ganadero_Animal_Codigo,
                                 Evento_Ganadero_Codigo = e.Evento_Ganadero_Codigo,
                                 Evento_Ganadero_Tipo = e.Evento_Ganadero_Tipo,
                                 Evento_Ganadero_Fecha = e.Evento_Ganadero_Fecha,
                                 Evento_Ganadero_Animal_Estado_Afectacion = ea.Evento_Ganadero_Animal_Estado_Afectacion,
                                 Evento_Ganadero_Registrado_Por = e.Evento_Ganadero_Registrado_Por,
                                 Evento_Ganadero_Observacion = e.Evento_Ganadero_Observacion,
                                 Evento_Ganadero_Es_Correccion = e.Evento_Ganadero_Es_Correccion,
                                 Evento_Ganadero_Es_Anulacion = e.Evento_Ganadero_Es_Anulacion
                             }).ToListAsync(cancellationToken);

        return eventos;
    }
}
