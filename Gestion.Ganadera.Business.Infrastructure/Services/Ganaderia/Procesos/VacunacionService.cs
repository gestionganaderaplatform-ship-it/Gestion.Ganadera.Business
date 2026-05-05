using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class VacunacionService(
    IVacunacionRepository repository,
    ICurrentActorProvider currentActorProvider) : IVacunacionService
{
    public async Task<bool> RegistrarAsync(
        RegistrarVacunacionRequest request,
        CancellationToken cancellationToken = default)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var eventos = request.Animales_Codigos.Select(animalCodigo => new EventoGanadero
        {
            Finca_Codigo = request.Finca_Codigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.Vacunacion,
            Evento_Ganadero_Fecha = request.Fecha_Aplicacion,
            Evento_Ganadero_Fecha_Registro = fechaOperacion,
            Evento_Ganadero_Registrado_Por = usuarioLogueado,
            Evento_Ganadero_Estado = EventoGanaderoEstado.Completado,
            Evento_Ganadero_Observacion = request.Observacion,
            Evento_Ganadero_Es_Correccion = false,
            Evento_Ganadero_Es_Anulacion = false
        }).ToList();

        var eventosAnimal = request.Animales_Codigos.Select(animalCodigo => new EventoGanaderoAnimal
        {
            Animal_Codigo = animalCodigo,
            Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
        }).ToList();

        var vacunadorEfectivo = request.Vacunador ?? usuarioLogueado;

        var detalles = request.Animales_Codigos.Select(animalCodigo => new EventoDetalleVacunacion
        {
            Evento_Detalle_Vacunacion_Fecha = request.Fecha_Aplicacion,
            Evento_Detalle_Vacunacion_Vacuna_Codigo = request.Vacuna_Codigo,
            Evento_Detalle_Vacunacion_Enfermedad_Codigo = request.Vacuna_Enfermedad_Codigo,
            Evento_Detalle_Vacunacion_Ciclo = request.Ciclo_Vacunacion ?? string.Empty,
            Evento_Detalle_Vacunacion_Lote = request.Lote_Biologico ?? string.Empty,
            Evento_Detalle_Vacunacion_Vacunador = vacunadorEfectivo,
            Evento_Detalle_Vacunacion_Dosis = request.Dosis,
            Evento_Detalle_Vacunacion_Soporte_Nombre = request.Soporte_Certificado_Nombre,
            Evento_Detalle_Vacunacion_Observacion = request.Observacion
        }).ToList();

        return await repository.RegistrarAtomicoAsync(
            eventos,
            eventosAnimal,
            detalles,
            cancellationToken);
    }

    public async Task<bool> ValidarAsync(
        ValidarVacunacionRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(true);
    }

    public async Task<bool> RegistrarLoteAsync(
        RegistrarVacunacionLoteRequest request,
        CancellationToken cancellationToken = default)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var animalCodigos = request.Animales.Select(a => a.Animal_Codigo).ToList();

        var eventos = animalCodigos.Select(animalCodigo => new EventoGanadero
        {
            Finca_Codigo = request.Finca_Codigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.Vacunacion,
            Evento_Ganadero_Fecha = request.Fecha_Aplicacion,
            Evento_Ganadero_Fecha_Registro = fechaOperacion,
            Evento_Ganadero_Registrado_Por = usuarioLogueado,
            Evento_Ganadero_Estado = EventoGanaderoEstado.Completado,
            Evento_Ganadero_Observacion = request.Observacion,
            Evento_Ganadero_Es_Correccion = false,
            Evento_Ganadero_Es_Anulacion = false
        }).ToList();

        var eventosAnimal = animalCodigos.Select(animalCodigo => new EventoGanaderoAnimal
        {
            Animal_Codigo = animalCodigo,
            Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
        }).ToList();

        var vacunadorEfectivo = request.Vacunador ?? usuarioLogueado;

        var detalles = animalCodigos.Select(animalCodigo => new EventoDetalleVacunacion
        {
            Evento_Detalle_Vacunacion_Fecha = request.Fecha_Aplicacion,
            Evento_Detalle_Vacunacion_Vacuna_Codigo = request.Vacuna_Codigo,
            Evento_Detalle_Vacunacion_Enfermedad_Codigo = request.Vacuna_Enfermedad_Codigo,
            Evento_Detalle_Vacunacion_Ciclo = request.Ciclo_Vacunacion ?? string.Empty,
            Evento_Detalle_Vacunacion_Lote = request.Lote_Biologico ?? string.Empty,
            Evento_Detalle_Vacunacion_Vacunador = vacunadorEfectivo,
            Evento_Detalle_Vacunacion_Dosis = request.Dosis,
            Evento_Detalle_Vacunacion_Soporte_Nombre = request.Soporte_Certificado_Nombre,
            Evento_Detalle_Vacunacion_Observacion = request.Observacion
        }).ToList();

        return await repository.RegistrarAtomicoAsync(
            eventos,
            eventosAnimal,
            detalles,
            cancellationToken);
    }
}