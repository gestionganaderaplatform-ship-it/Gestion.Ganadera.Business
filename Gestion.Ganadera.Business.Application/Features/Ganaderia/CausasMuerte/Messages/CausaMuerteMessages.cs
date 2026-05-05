namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Messages;

public static class CausaMuerteMessages
{
    public const string NombreRequerido = "El nombre de la causa de muerte es obligatorio.";
    public const string NombreExcedeLongitud = "El nombre de la causa de muerte no puede exceder los 100 caracteres.";
    public const string NombreDuplicado = "Ya existe una causa de muerte con este nombre.";
    public const string DescripcionExcedeLongitud = "La descripción no puede exceder los 500 caracteres.";
    public const string CausaNoEncontrada = "La causa de muerte seleccionada no existe.";
}
