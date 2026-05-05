namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Messages;

public static class PalpacionMessages
{
    public const string AnimalNoEncontrado = "El animal no existe en el sistema.";
    public const string AnimalInactivo = "No se puede registrar palpación para un animal inactivo.";
    public const string ResultadoNoEncontrado = "El resultado seleccionado no existe.";
    public const string FechaObligatoria = "La fecha de la revisión es obligatoria.";
    public const string AnimalNoElegible = "El animal no es elegible para revisión reproductiva (Debe ser hembra de categoría reproductiva).";
    public const string PalpacionRegistrada = "Revisión reproductiva registrada correctamente.";
}
