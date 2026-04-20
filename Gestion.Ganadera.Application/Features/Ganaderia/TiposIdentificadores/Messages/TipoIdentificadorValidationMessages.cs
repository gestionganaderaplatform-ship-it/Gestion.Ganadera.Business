namespace Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Messages;

public static class TipoIdentificadorValidationMessages
{
    public const string TipoIdentificadorNoExiste = "El tipo de identificador indicado no existe.";
    public const string TipoIdentificadorNombreDuplicado = "Ya existe un tipo de identificador con ese nombre.";
    public const string TipoIdentificadorNombreFormatoInvalido = "El nombre del tipo contiene caracteres no permitidos.";
    public const string TipoIdentificadorNombreNoDebeEmpezarOTerminarConEspacios = "El nombre del tipo no debe empezar ni terminar con espacios.";
    public const string TipoIdentificadorCodigoInvalido = "El codigo del tipo de identificador debe ser mayor que cero.";
}
