using Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia;

public class AnimalConsultaRepository(AppDbContext context) : IAnimalConsultaRepository
{
    public async Task<InicioDashboardViewModel> ObtenerResumenInicioAsync(
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        var hoy = DateTime.UtcNow.Date;
        var fechaIngresoDesde = hoy.AddDays(-30);
        var fechaEventoDesde = hoy.AddDays(-7);

        var animales = context.Animales.AsNoTracking();
        var eventos = context.EventosGanaderos.AsNoTracking();

        if (fincaCodigo.HasValue)
        {
            animales = animales.Where(animal => animal.Finca_Codigo == fincaCodigo.Value);
            eventos = eventos.Where(evento => evento.Finca_Codigo == fincaCodigo.Value);
        }

        var animalesActivos = animales.Where(animal => animal.Animal_Activo);
        var totalActivos = await animalesActivos.CountAsync(cancellationToken);

        var potrerosConAnimales = await animalesActivos
            .Select(animal => animal.Potrero_Codigo)
            .Distinct()
            .CountAsync(cancellationToken);

        var ingresosUltimos30Dias = await animales
            .CountAsync(
                animal => animal.Animal_Fecha_Ingreso_Inicial >= fechaIngresoDesde,
                cancellationToken);

        var eventosUltimos7Dias = await eventos
            .CountAsync(
                evento => evento.Evento_Ganadero_Fecha >= fechaEventoDesde,
                cancellationToken);

        var distribucionCategoriasRaw = await (from animal in animalesActivos
                                               join categoria in context.CategoriasAnimal.AsNoTracking()
                                                   on animal.Categoria_Animal_Codigo equals categoria.Categoria_Animal_Codigo
                                               group animal by categoria.Categoria_Animal_Nombre into grouped
                                               orderby grouped.Count() descending, grouped.Key
                                               select new
                                               {
                                                   Nombre = grouped.Key,
                                                   Cantidad = grouped.Count()
                                               })
            .Take(6)
            .ToListAsync(cancellationToken);

        var distribucionPotrerosRaw = await (from animal in animalesActivos
                                             join potrero in context.Potreros.AsNoTracking()
                                                 on animal.Potrero_Codigo equals potrero.Potrero_Codigo
                                             group animal by potrero.Potrero_Nombre into grouped
                                             orderby grouped.Count() descending, grouped.Key
                                             select new
                                             {
                                                 Nombre = grouped.Key,
                                                 Cantidad = grouped.Count()
                                             })
            .Take(6)
            .ToListAsync(cancellationToken);

        var actividadReciente = await (from eventoAnimal in context.EventosGanaderosAnimal.AsNoTracking()
                                       join evento in eventos
                                           on eventoAnimal.Evento_Ganadero_Codigo equals evento.Evento_Ganadero_Codigo
                                       join animal in animales
                                           on eventoAnimal.Animal_Codigo equals animal.Animal_Codigo
                                       join potrero in context.Potreros.AsNoTracking()
                                           on animal.Potrero_Codigo equals potrero.Potrero_Codigo
                                       from principalIdent in context.IdentificadoresAnimal.AsNoTracking()
                                           .Where(i => i.Animal_Codigo == animal.Animal_Codigo
                                                    && i.Identificador_Animal_Es_Principal
                                                    && i.Identificador_Animal_Activo)
                                           .OrderByDescending(i => i.Identificador_Animal_Codigo)
                                           .Take(1)
                                           .DefaultIfEmpty()
                                       orderby evento.Evento_Ganadero_Fecha_Registro descending
                                       select new InicioDashboardActividadViewModel
                                       {
                                           Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo,
                                           Evento_Ganadero_Tipo = evento.Evento_Ganadero_Tipo,
                                           Evento_Ganadero_Fecha = evento.Evento_Ganadero_Fecha,
                                           Animal_Identificador_Principal =
                                               principalIdent != null
                                                   ? principalIdent.Identificador_Animal_Valor
                                                   : "Sin identificador",
                                           Potrero_Nombre = potrero.Potrero_Nombre
                                       })
            .Take(6)
            .ToListAsync(cancellationToken);

        var lecturasOperativas = BuildLecturasOperativas(totalActivos, eventosUltimos7Dias);

        return new InicioDashboardViewModel
        {
            Animales_Activos = totalActivos,
            Potreros_Con_Animales = potrerosConAnimales,
            Ingresos_Ultimos_30_Dias = ingresosUltimos30Dias,
            Eventos_Ultimos_7_Dias = eventosUltimos7Dias,
            Lecturas_Operativas = lecturasOperativas,
            Distribucion_Categorias = MapDistribucion(distribucionCategoriasRaw, totalActivos),
            Distribucion_Potreros = MapDistribucion(distribucionPotrerosRaw, totalActivos),
            Actividad_Reciente = actividadReciente
        };
    }

    public async Task<(IEnumerable<GanadoViewModel> Items, int TotalRegistros)> ObtenerPorPaginado(
        int pagina,
        int tamanoPagina,
        long? fincaCodigo = null,
        string? busqueda = null,
        string? animalIdentificadorPrincipal = null,
        string? categoriaAnimalNombre = null,
        string? potreroNombre = null,
        DateTime? animalFechaIngresoInicial = null,
        CancellationToken cancellationToken = default)
    {
        var animales = context.Animales.AsNoTracking();

        if (fincaCodigo.HasValue)
        {
            animales = animales.Where(animal => animal.Finca_Codigo == fincaCodigo.Value);
        }

        var query = from animal in animales
                    join potrero in context.Potreros.AsNoTracking() on animal.Potrero_Codigo equals potrero.Potrero_Codigo
                    join categoria in context.CategoriasAnimal.AsNoTracking() on animal.Categoria_Animal_Codigo equals categoria.Categoria_Animal_Codigo
                    from principalIdent in context.IdentificadoresAnimal.AsNoTracking()
                        .Where(i => i.Animal_Codigo == animal.Animal_Codigo
                                 && i.Identificador_Animal_Es_Principal
                                 && i.Identificador_Animal_Activo)
                        .OrderByDescending(i => i.Identificador_Animal_Codigo)
                        .Take(1)
                        .DefaultIfEmpty()
                    select new GanadoViewModel
                    {
                        Animal_Codigo = animal.Animal_Codigo,
                        Animal_Identificador_Principal = principalIdent != null ? principalIdent.Identificador_Animal_Valor : "Sin identificador",
                        Categoria_Animal_Nombre = categoria.Categoria_Animal_Nombre,
                        Potrero_Nombre = potrero.Potrero_Nombre,
                        Animal_Origen_Ingreso = animal.Animal_Origen_Ingreso,
                        Animal_Fecha_Ingreso_Inicial = animal.Animal_Fecha_Ingreso_Inicial,
                        Animal_Activo = animal.Animal_Activo
                    };

        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            var term = busqueda.Trim().ToLower();
            query = query.Where(x =>
                x.Animal_Identificador_Principal.ToLower().Contains(term) ||
                x.Categoria_Animal_Nombre.ToLower().Contains(term) ||
                x.Potrero_Nombre.ToLower().Contains(term)
            );
        }

        // Aplicamos filtros específicos si vienen
        if (!string.IsNullOrWhiteSpace(animalIdentificadorPrincipal))
        {
            var term = animalIdentificadorPrincipal.Trim().ToLower();
            query = query.Where(x => x.Animal_Identificador_Principal.ToLower().Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(categoriaAnimalNombre))
        {
            var term = categoriaAnimalNombre.Trim().ToLower();
            query = query.Where(x => x.Categoria_Animal_Nombre.ToLower().Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(potreroNombre))
        {
            var term = potreroNombre.Trim().ToLower();
            query = query.Where(x => x.Potrero_Nombre.ToLower().Contains(term));
        }

        if (animalFechaIngresoInicial.HasValue)
        {
            var fecha = animalFechaIngresoInicial.Value.Date;
            query = query.Where(x => x.Animal_Fecha_Ingreso_Inicial.Date == fecha);
        }

        var totalRegistros = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.Animal_Codigo)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(cancellationToken);

        return (items, totalRegistros);
    }

    public async Task<AnimalViewModel?> Consultar(
        long animalCodigo,
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        var animales = context.Animales.AsNoTracking()
            .Where(animal => animal.Animal_Codigo == animalCodigo);

        if (fincaCodigo.HasValue)
        {
            animales = animales.Where(animal => animal.Finca_Codigo == fincaCodigo.Value);
        }

        var query = from animal in animales
                    join finca in context.Fincas.AsNoTracking() on animal.Finca_Codigo equals finca.Finca_Codigo
                    join potrero in context.Potreros.AsNoTracking() on animal.Potrero_Codigo equals potrero.Potrero_Codigo
                    join categoria in context.CategoriasAnimal.AsNoTracking() on animal.Categoria_Animal_Codigo equals categoria.Categoria_Animal_Codigo
                    from principalIdent in context.IdentificadoresAnimal.AsNoTracking()
                        .Where(i => i.Animal_Codigo == animal.Animal_Codigo
                                 && i.Identificador_Animal_Es_Principal
                                 && i.Identificador_Animal_Activo)
                        .OrderByDescending(i => i.Identificador_Animal_Codigo)
                        .Take(1)
                        .DefaultIfEmpty()
                    let totalPartos = context.AnimalesRelacionesFamiliares
                        .IgnoreQueryFilters()
                        .AsNoTracking()
                        .Count(relacion =>
                            relacion.Animal_Codigo_Madre == animal.Animal_Codigo &&
                            relacion.Animal_Relacion_Familiar_Activa &&
                            relacion.Animal_Relacion_Familiar_Tipo == AnimalRelacionFamiliarTipo.MadreCria)
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
                        Animal_Fecha_Ultimo_Evento = animal.Animal_Fecha_Ultimo_Evento,
                        Animal_Total_Partos = totalPartos
                    };

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<AnimalHistorialViewModel>> ObtenerHistorialAsync(
        long animalCodigo,
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        var animales = context.Animales.AsNoTracking()
            .Where(animal => animal.Animal_Codigo == animalCodigo);

        if (fincaCodigo.HasValue)
        {
            animales = animales.Where(animal => animal.Finca_Codigo == fincaCodigo.Value);
        }

        var eventos = await (from animal in animales
                             join ea in context.EventosGanaderosAnimal.AsNoTracking()
                               on animal.Animal_Codigo equals ea.Animal_Codigo
                             join e in context.EventosGanaderos.AsNoTracking()
                               on ea.Evento_Ganadero_Codigo equals e.Evento_Ganadero_Codigo
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

    public async Task<IEnumerable<MadreElegibleViewModel>> ObtenerMadresElegiblesAsync(
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        var animales = context.Animales.AsNoTracking()
            .Where(animal => animal.Animal_Activo);

        if (fincaCodigo.HasValue)
        {
            animales = animales.Where(animal => animal.Finca_Codigo == fincaCodigo.Value);
        }

        var query = from animal in animales
                    join potrero in context.Potreros.AsNoTracking() on animal.Potrero_Codigo equals potrero.Potrero_Codigo
                    join categoria in context.CategoriasAnimal.AsNoTracking() on animal.Categoria_Animal_Codigo equals categoria.Categoria_Animal_Codigo
                    from principalIdent in context.IdentificadoresAnimal.AsNoTracking()
                        .Where(i => i.Animal_Codigo == animal.Animal_Codigo
                                 && i.Identificador_Animal_Es_Principal
                                 && i.Identificador_Animal_Activo)
                        .OrderByDescending(i => i.Identificador_Animal_Codigo)
                        .Take(1)
                        .DefaultIfEmpty()
                    where animal.Animal_Sexo.ToUpper() == "HEMBRA"
                       && (
                           categoria.Categoria_Animal_Nombre.ToUpper().Contains("VACA")
                           || categoria.Categoria_Animal_Nombre.ToUpper().Contains("NOVILLA")
                       )
                    select new MadreElegibleViewModel
                    {
                        Animal_Codigo = animal.Animal_Codigo,
                        Animal_Identificador_Principal = principalIdent != null ? principalIdent.Identificador_Animal_Valor : "Sin identificador",
                        Categoria_Animal_Nombre = categoria.Categoria_Animal_Nombre,
                        Potrero_Codigo = potrero.Potrero_Codigo,
                        Potrero_Nombre = potrero.Potrero_Nombre,
                        Animal_Sexo = animal.Animal_Sexo
                    };

        return await query
            .OrderBy(x => x.Animal_Identificador_Principal)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IEnumerable<GanadoViewModel> Items, int TotalRegistros)> FiltrarPaginado(
        int pagina,
        int tamanoPagina,
        long? fincaCodigo,
        AnimalConsultaFilterViewModel filtro,
        CancellationToken cancellationToken = default)
    {
        var animales = context.Animales.AsNoTracking();

        if (fincaCodigo.HasValue)
        {
            animales = animales.Where(animal => animal.Finca_Codigo == fincaCodigo.Value);
        }

        var query = from animal in animales
                    join potrero in context.Potreros.AsNoTracking() on animal.Potrero_Codigo equals potrero.Potrero_Codigo
                    join categoria in context.CategoriasAnimal.AsNoTracking() on animal.Categoria_Animal_Codigo equals categoria.Categoria_Animal_Codigo
                    from principalIdent in context.IdentificadoresAnimal.AsNoTracking()
                        .Where(i => i.Animal_Codigo == animal.Animal_Codigo
                                 && i.Identificador_Animal_Es_Principal
                                 && i.Identificador_Animal_Activo)
                        .OrderByDescending(i => i.Identificador_Animal_Codigo)
                        .Take(1)
                        .DefaultIfEmpty()
                    select new GanadoViewModel
                    {
                        Animal_Codigo = animal.Animal_Codigo,
                        Animal_Identificador_Principal = principalIdent != null ? principalIdent.Identificador_Animal_Valor : "Sin identificador",
                        Categoria_Animal_Nombre = categoria.Categoria_Animal_Nombre,
                        Potrero_Nombre = potrero.Potrero_Nombre,
                        Animal_Origen_Ingreso = animal.Animal_Origen_Ingreso,
                        Animal_Fecha_Ingreso_Inicial = animal.Animal_Fecha_Ingreso_Inicial,
                        Animal_Activo = animal.Animal_Activo
                    };

        if (!string.IsNullOrWhiteSpace(filtro.Animal_Identificador_Principal))
        {
            var term = filtro.Animal_Identificador_Principal.Trim().ToLower();
            query = query.Where(x => x.Animal_Identificador_Principal.ToLower().Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(filtro.Categoria_Animal_Nombre))
        {
            var term = filtro.Categoria_Animal_Nombre.Trim().ToLower();
            query = query.Where(x => x.Categoria_Animal_Nombre.ToLower().Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(filtro.Potrero_Nombre))
        {
            var term = filtro.Potrero_Nombre.Trim().ToLower();
            query = query.Where(x => x.Potrero_Nombre.ToLower().Contains(term));
        }

        if (filtro.Animal_Fecha_Ingreso_Inicial.HasValue)
        {
            var fecha = filtro.Animal_Fecha_Ingreso_Inicial.Value.Date;
            query = query.Where(x => x.Animal_Fecha_Ingreso_Inicial.Date == fecha);
        }

        var totalRegistros = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(x => x.Animal_Codigo)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync(cancellationToken);

        return (items, totalRegistros);
    }

    private static IEnumerable<InicioDashboardDistribucionViewModel> MapDistribucion<T>(
        IEnumerable<T> items,
        int totalActivos)
        where T : class
    {
        return items.Select(item =>
        {
            var nombre = (string)item.GetType().GetProperty("Nombre")!.GetValue(item)!;
            var cantidad = (int)item.GetType().GetProperty("Cantidad")!.GetValue(item)!;

            return new InicioDashboardDistribucionViewModel
            {
                Nombre = nombre,
                Cantidad = cantidad,
                Porcentaje = totalActivos <= 0
                    ? 0
                    : Math.Round((decimal)cantidad * 100 / totalActivos, 1)
            };
        });
    }

    private static IEnumerable<string> BuildLecturasOperativas(int totalActivos, int eventosUltimos7Dias)
    {
        var notas = new List<string>();

        if (totalActivos == 0)
        {
            notas.Add("Aun no hay animales activos en esta finca. El siguiente paso natural es registrar existente.");
        }

        if (eventosUltimos7Dias == 0)
        {
            notas.Add("No hay eventos recientes. Cuando empieces a registrar movimientos y procesos, apareceran aqui.");
        }

        notas.Add("Ya tienes una base para seguir con compra, venta, movimientos y sanidad sin repetir contexto.");
        return notas;
    }
}
