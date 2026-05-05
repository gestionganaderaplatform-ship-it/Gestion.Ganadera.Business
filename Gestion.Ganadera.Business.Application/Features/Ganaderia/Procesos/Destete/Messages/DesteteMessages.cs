namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Messages;

public static class DesteteMessages
{
    public const string AnimalNoEncontrado = "El animal (cría) no existe.";
    public const string MadreNoEncontrada = "La madre especificada no existe.";
    public const string AnimalInactivo = "No se puede destetar un animal inactivo.";
    public const string FechaObligatoria = "La fecha de destete es obligatoria.";
    public const string PotreroInvalido = "El potrero de destino no es válido para esta finca.";
    public const string DesteteRegistrado = "Destete registrado correctamente.";
    public const string ItemsRequeridos = "Debe incluir al menos un animal para registrar.";
}
