namespace Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Messages;

public static class RangoEdadValidationMessages
{
    public const string RangoEdadNoExiste = "El rango de edad indicado no existe.";
    public const string RangoEdadNombreDuplicado = "Ya existe un rango de edad con ese nombre.";
    public const string RangoEdadNombreFormatoInvalido = "El nombre del rango contiene caracteres no permitidos.";
    public const string RangoEdadNombreNoDebeEmpezarOTerminarConEspacios = "El nombre del rango no debe empezar ni terminar con espacios.";
    public const string RangoEdadCodigoInvalido = "El codigo del rango de edad debe ser mayor que cero.";
}
