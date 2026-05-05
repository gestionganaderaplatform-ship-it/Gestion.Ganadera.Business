using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class CambioCategoriaService(
    ICambioCategoriaRepository repository,
    ICurrentActorProvider currentActorProvider,
    ICurrentClientProvider currentClientProvider) : ICambioCategoriaService
{
    public async Task<int> GenerarSugerenciasAsync(long fincaCodigo, CancellationToken cancellationToken = default)
    {
        var clienteCodigo = currentClientProvider.ClientNumericId;
        if (clienteCodigo == null) return 0;

        var animales = await repository.ObtenerCandidatosCambioAsync(fincaCodigo, cancellationToken);
        var categorias = await repository.ObtenerCategoriasConReglasAsync(cancellationToken);
        
        // Obtener IDs de animales que ya tienen una sugerencia pendiente para evitar duplicados
        var sugerenciasExistentes = await repository.ObtenerSugerenciasPendientesDetalladasAsync(fincaCodigo, cancellationToken);
        var idsConSugerencia = sugerenciasExistentes.Select(s => s.Animal_Codigo).ToHashSet();

        var sugerenciasParaCrear = new List<CambioCategoriaSugerido>();
        var fechaActual = DateTime.Now;

        foreach (var animal in animales)
        {
            if (idsConSugerencia.Contains(animal.Animal_Codigo)) continue;

            var regla = categorias.FirstOrDefault(c => c.Categoria_Animal_Codigo == animal.Categoria_Animal_Codigo);
            if (regla == null || regla.Categoria_Animal_Siguiente_Codigo == null) continue;

            // Calcular edad meses (con fallback a fecha de ingreso)
            var fechaBase = animal.Animal_Fecha_Nacimiento ?? animal.Animal_Fecha_Ingreso_Inicial;
            var edadMeses = (int)((fechaActual - fechaBase).TotalDays / 30.44);

            if (edadMeses >= (regla.Categoria_Animal_Meses_Sugeridos ?? 0))
            {
                sugerenciasParaCrear.Add(new CambioCategoriaSugerido
                {
                    Cliente_Codigo = clienteCodigo,
                    Animal_Codigo = animal.Animal_Codigo,
                    Categoria_Actual_Codigo = animal.Categoria_Animal_Codigo,
                    Categoria_Sugerida_Codigo = regla.Categoria_Animal_Siguiente_Codigo.Value,
                    Sugerencia_Motivo = CambioCategoriaSugerenciaMotivo.EdadPermanencia,
                    Sugerencia_Estado = CambioCategoriaSugerenciaEstado.Pendiente,
                    Fecha_Sugerencia = fechaActual
                });
            }
        }

        if (sugerenciasParaCrear.Count == 0) return 0;

        return await repository.RegistrarSugerenciasMasivamenteAsync(sugerenciasParaCrear, cancellationToken);
    }

    public async Task<IEnumerable<object>> ObtenerSugerenciasPendientesAsync(long fincaCodigo, CancellationToken cancellationToken = default)
    {
        return await repository.ObtenerSugerenciasPendientesDetalladasAsync(fincaCodigo, cancellationToken);
    }

    public async Task<bool> AprobarSugerenciasAsync(IEnumerable<long> sugerenciasCodigos, CancellationToken cancellationToken = default)
    {
        var actor = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;
        var exitoGlobal = true;

        // Por simplicidad en esta fase, procesamos uno a uno. 
        // En una etapa posterior se podria optimizar para procesar en un solo lote transaccional.
        foreach (var codigo in sugerenciasCodigos)
        {
            // 1. Obtener la sugerencia con sus datos relacionados
            // (Usamos el repo para buscarla. Nota: Idealmente el repo tendria un metodo GetById con Includes)
            // Para cumplir con AGENTS.md, lo haremos de forma eficiente.
            
            var sugerencia = await repository.ObtenerSugerenciaPorCodigoAsync(codigo, cancellationToken);
            if (sugerencia == null || sugerencia.Sugerencia_Estado != CambioCategoriaSugerenciaEstado.Pendiente) continue;

            var animal = sugerencia.Animal;
            if (animal == null) continue;

            // 2. Preparar el cambio en el Animal
            animal.Categoria_Animal_Codigo = sugerencia.Categoria_Sugerida_Codigo;
            animal.Animal_Fecha_Ultimo_Evento = fechaOperacion;

            // 3. Crear el Evento Ganadero
            var evento = new EventoGanadero
            {
                Cliente_Codigo = sugerencia.Cliente_Codigo,
                Finca_Codigo = animal.Finca_Codigo,
                Evento_Ganadero_Tipo = EventoGanaderoTipo.CambioCategoria,
                Evento_Ganadero_Fecha = fechaOperacion,
                Evento_Ganadero_Fecha_Registro = fechaOperacion,
                Evento_Ganadero_Registrado_Por = actor,
                Evento_Ganadero_Estado = EventoGanaderoEstado.Completado,
                Evento_Ganadero_Observacion = $"Cambio automático sugerido por {sugerencia.Sugerencia_Motivo}",
                Evento_Ganadero_Es_Correccion = false,
                Evento_Ganadero_Es_Anulacion = false
            };

            var eventoAnimal = new EventoGanaderoAnimal
            {
                Cliente_Codigo = sugerencia.Cliente_Codigo,
                Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
            };

            var detalle = new EventoDetalleCambioCategoria
            {
                Cliente_Codigo = sugerencia.Cliente_Codigo,
                Categoria_Anterior_Codigo = sugerencia.Categoria_Actual_Codigo,
                Categoria_Nueva_Codigo = sugerencia.Categoria_Sugerida_Codigo,
                Evento_Detalle_Cambio_Categoria_Peso_Al_Cambio = animal.Animal_Peso,
                Evento_Detalle_Cambio_Categoria_Observacion = "Proceso automatizado de cambio de categoría"
            };

            // 4. Aplicar mediante el repositorio (Atómico)
            var exito = await repository.AplicarCambioCategoriaAtomicoAsync(
                animal, evento, eventoAnimal, detalle, sugerencia, cancellationToken);
            
            if (!exito) exitoGlobal = false;
        }

        return exitoGlobal;
    }

    public async Task<bool> RechazarSugerenciasAsync(IEnumerable<long> sugerenciasCodigos, CancellationToken cancellationToken = default)
    {
        return await repository.RechazarSugerenciasAsync(sugerenciasCodigos, cancellationToken);
    }
}
