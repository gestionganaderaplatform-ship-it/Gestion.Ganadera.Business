namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.Messages;

public static class PotreroValidationMessages
{
    public const string PotreroNoExiste = "El potrero indicado no existe.";
    public const string FincaNoExiste = "La finca indicada no existe.";
    public const string PotreroNombreDuplicado = "Ya existe un potrero con ese nombre en la finca seleccionada.";
    public const string PotreroNombreFormatoInvalido = "El nombre del potrero contiene caracteres no permitidos.";
    public const string PotreroNombreNoDebeEmpezarOTerminarConEspacios = "El nombre del potrero no debe empezar ni terminar con espacios.";
    public const string PotreroCodigoInvalido = "El codigo del potrero debe ser mayor que cero.";
    public const string FincaCodigoInvalido = "El codigo de la finca debe ser mayor que cero.";
}
