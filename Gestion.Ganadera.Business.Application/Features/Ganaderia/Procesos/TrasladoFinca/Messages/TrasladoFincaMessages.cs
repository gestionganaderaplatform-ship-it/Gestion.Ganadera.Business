namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Messages;

public static class TrasladoFincaMessages
{
    public const string FincaOrigenObligatoria = "La finca de origen es obligatoria.";
    public const string FincaDestinoObligatoria = "La finca de destino es obligatoria.";
    public const string PotreroDestinoObligatorio = "El potrero destino es obligatorio.";
    public const string AnimalesObligatorios = "Debe seleccionar al menos un animal.";
    public const string AnimalNoEncontrado = "El animal no existe.";
    public const string AnimalInactivo = "El animal no está activo.";
    public const string FincaDestinoIgualOrigen = "La finca de destino no puede ser la misma de origen.";
    public const string TrasladoRegistrado = "Traslado entre fincas registrado correctamente.";
    public const string TrasladoLoteRegistrado = "Traslado entre fincas registrado para {0} animales.";
    public const string FechaObligatoria = "La fecha del traslado es obligatoria.";
    public const string FechaFutura = "No se pueden registrar traslados futuros.";
}
