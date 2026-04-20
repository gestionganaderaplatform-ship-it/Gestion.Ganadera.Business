namespace Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Messages;

public static class FincaValidationMessages
{
    public const string FincaNoExiste = "La finca indicada no existe.";
    public const string FincaNombreDuplicado = "Ya existe una finca con ese nombre para este cliente.";
    public const string FincaNombreFormatoInvalido = "El nombre de la finca contiene caracteres no permitidos.";
    public const string FincaNombreNoDebeEmpezarOTerminarConEspacios = "El nombre de la finca no debe empezar ni terminar con espacios.";
    public const string FincaCodigoInvalido = "El codigo de la finca debe ser mayor que cero.";
}
