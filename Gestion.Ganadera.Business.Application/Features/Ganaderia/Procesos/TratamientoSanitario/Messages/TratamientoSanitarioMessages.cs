namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Messages;

public static class TratamientoSanitarioMessages
{
    public const string AnimalNoEncontrado = "El animal no existe en el sistema.";
    public const string AnimalInactivo = "No se puede registrar tratamiento para un animal inactivo.";
    public const string ProductoNoEncontrado = "El producto o tratamiento no existe.";
    public const string FechaObligatoria = "La fecha de aplicación es obligatoria.";
    public const string TratamientoRegistrado = "Tratamiento registrado correctamente.";
    public const string TratamientoLoteRegistrado = "Tratamiento registrado correctamente para {0} animales.";
}
