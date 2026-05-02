using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class NacimientoService(
    INacimientoRepository repository,
    ICurrentActorProvider currentActorProvider) : INacimientoService
{
    public Task<bool> RegistrarAsync(
        RegistrarNacimientoRequest request,
        CancellationToken cancellationToken = default)
    {
        var entidades = CrearEntidades(request);

        return repository.RegistrarAtomicoAsync(
            entidades.Cria,
            entidades.Identificador,
            entidades.Evento,
            entidades.EventoAnimal,
            entidades.Detalle,
            entidades.Relacion,
            cancellationToken);
    }

    public Task<int> ObtenerSiguienteConsecutivoAsync(
        long fincaCodigo,
        CancellationToken cancellationToken = default)
    {
        return repository.ObtenerSiguienteConsecutivoAsync(fincaCodigo, cancellationToken);
    }

    private (Animal Cria, IdentificadorAnimal Identificador, EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleNacimiento Detalle, AnimalRelacionFamiliar Relacion) CrearEntidades(
        RegistrarNacimientoRequest request)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;
        var identificadorNormalizado = request.Identificador_Principal.Trim();
        var sexoNormalizado = NormalizarSexo(request.Animal_Sexo);

        var cria = new Animal
        {
            Finca_Codigo = request.Finca_Codigo,
            Potrero_Codigo = request.Potrero_Codigo,
            Categoria_Animal_Codigo = request.Categoria_Animal_Codigo,
            Animal_Sexo = sexoNormalizado,
            Animal_Origen_Ingreso = AnimalOrigenIngreso.Nacimiento,
            Animal_Activo = true,
            Animal_Fecha_Nacimiento = request.Fecha_Nacimiento,
            Animal_Fecha_Ingreso_Inicial = request.Fecha_Nacimiento,
            Animal_Fecha_Registro_Ingreso = fechaOperacion,
            Animal_Fecha_Ultimo_Evento = request.Fecha_Nacimiento
        };

        var identificador = new IdentificadorAnimal
        {
            Tipo_Identificador_Codigo = request.Tipo_Identificador_Codigo,
            Identificador_Animal_Valor = identificadorNormalizado,
            Identificador_Animal_Es_Principal = true,
            Identificador_Animal_Activo = true
        };

        var evento = new EventoGanadero
        {
            Finca_Codigo = request.Finca_Codigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.Nacimiento,
            Evento_Ganadero_Fecha = request.Fecha_Nacimiento,
            Evento_Ganadero_Fecha_Registro = fechaOperacion,
            Evento_Ganadero_Registrado_Por = usuarioLogueado,
            Evento_Ganadero_Estado = EventoGanaderoEstado.Completado,
            Evento_Ganadero_Observacion = request.Observacion,
            Evento_Ganadero_Es_Correccion = false,
            Evento_Ganadero_Es_Anulacion = false
        };

        var eventoAnimal = new EventoGanaderoAnimal
        {
            Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
        };

        var detalle = new EventoDetalleNacimiento
        {
            Animal_Codigo_Madre = request.Madre_Animal_Codigo,
            Tipo_Identificador_Codigo = request.Tipo_Identificador_Codigo,
            Evento_Detalle_Nacimiento_Identificador_Valor = identificadorNormalizado,
            Categoria_Animal_Codigo = request.Categoria_Animal_Codigo,
            Potrero_Codigo = request.Potrero_Codigo,
            Evento_Detalle_Nacimiento_Sexo = sexoNormalizado,
            Evento_Detalle_Nacimiento_Fecha_Nacimiento = request.Fecha_Nacimiento,
            Evento_Detalle_Nacimiento_Peso_Nacer = request.Peso_Nacer,
            Evento_Detalle_Nacimiento_Observacion = request.Observacion
        };

        var relacion = new AnimalRelacionFamiliar
        {
            Animal_Codigo_Madre = request.Madre_Animal_Codigo,
            Animal_Relacion_Familiar_Tipo = AnimalRelacionFamiliarTipo.MadreCria,
            Animal_Relacion_Familiar_Fecha_Inicio = request.Fecha_Nacimiento,
            Animal_Relacion_Familiar_Activa = true
        };

        return (cria, identificador, evento, eventoAnimal, detalle, relacion);
    }

    private static string NormalizarSexo(string sexo)
    {
        return sexo.Trim().ToUpperInvariant() switch
        {
            "M" => "MACHO",
            "H" => "HEMBRA",
            var valor => valor
        };
    }
}
