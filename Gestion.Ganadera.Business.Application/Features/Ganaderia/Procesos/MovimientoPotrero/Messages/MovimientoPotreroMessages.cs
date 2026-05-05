namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Messages;

public static class MovimientoPotreroMessages
{
    public const string FincaObligatoria = "La finca es obligatoria.";
    public const string PotreroDestinoObligatorio = "El potrero destino es obligatorio.";
    public const string AnimalesObligatorios = "Debe seleccionar al menos un animal.";
    public const string AnimalNoEncontrado = "El animal no existe.";
    public const string AnimalInactivo = "El animal no está activo.";
    public const string PotreroOrigenIgualDestino = "El potrero destino no puede ser el mismo actual.";
    public const string MovimientoRegistrado = "Movimiento de potrero registrado correctamente.";
    public const string MovimientoLoteRegistrado = "Movimiento de potrero registrado para {0} animales.";
    public const string FechaObligatoria = "La fecha del movimiento es obligatoria.";
    public const string FechaFutura = "No se pueden registrar movimientos futuros.";
}