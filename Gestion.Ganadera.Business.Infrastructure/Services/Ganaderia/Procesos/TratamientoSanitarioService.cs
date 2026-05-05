using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class TratamientoSanitarioService(ITratamientoSanitarioRepository repository) : ITratamientoSanitarioService
{
    public async Task<bool> RegistrarAsync(RegistrarTratamientoRequest request, CancellationToken cancellationToken = default)
    {
        var evento = new EventoGanadero
        {
            Evento_Ganadero_Tipo = EventoGanaderoTipo.TratamientoSanitario,
            Evento_Ganadero_Fecha = request.Fecha_Aplicacion,
            Evento_Ganadero_Observacion = request.Observacion,
            Evento_Ganadero_Estado = EventoGanaderoEstado.Completado
        };

        var eventoAnimal = new EventoGanaderoAnimal
        {
            Animal_Codigo = request.Animal_Codigo,
            Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
        };

        var detalle = new EventoDetalleTratamientoSanitario
        {
            Tratamiento_Producto_Codigo = request.Tratamiento_Producto_Codigo,
            Evento_Detalle_Tratamiento_Fecha = request.Fecha_Aplicacion,
            Evento_Detalle_Tratamiento_Dosis = request.Dosis,
            Evento_Detalle_Tratamiento_Duracion = request.Duracion,
            Evento_Detalle_Tratamiento_Indicacion = request.Indicacion,
            Evento_Detalle_Tratamiento_Aplicador = request.Aplicador,
            Evento_Detalle_Tratamiento_Observacion = request.Observacion
        };

        return await repository.RegistrarAtomicoAsync([evento], [eventoAnimal], [detalle], cancellationToken);
    }

    public async Task<bool> RegistrarLoteAsync(RegistrarTratamientoLoteRequest request, CancellationToken cancellationToken = default)
    {
        var eventos = new List<EventoGanadero>();
        var eventosAnimal = new List<EventoGanaderoAnimal>();
        var detalles = new List<EventoDetalleTratamientoSanitario>();

        foreach (var animalCodigo in request.Animales)
        {
            eventos.Add(new EventoGanadero
            {
                Evento_Ganadero_Tipo = EventoGanaderoTipo.TratamientoSanitario,
                Evento_Ganadero_Fecha = request.Fecha_Aplicacion,
                Evento_Ganadero_Observacion = request.Observacion,
                Evento_Ganadero_Estado = EventoGanaderoEstado.Completado
            });

            eventosAnimal.Add(new EventoGanaderoAnimal
            {
                Animal_Codigo = animalCodigo,
                Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
            });

            detalles.Add(new EventoDetalleTratamientoSanitario
            {
                Tratamiento_Producto_Codigo = request.Tratamiento_Producto_Codigo,
                Evento_Detalle_Tratamiento_Fecha = request.Fecha_Aplicacion,
                Evento_Detalle_Tratamiento_Dosis = request.Dosis,
                Evento_Detalle_Tratamiento_Duracion = request.Duracion,
                Evento_Detalle_Tratamiento_Indicacion = request.Indicacion,
                Evento_Detalle_Tratamiento_Aplicador = request.Aplicador,
                Evento_Detalle_Tratamiento_Observacion = request.Observacion
            });
        }

        return await repository.RegistrarAtomicoAsync(eventos, eventosAnimal, detalles, cancellationToken);
    }
}
