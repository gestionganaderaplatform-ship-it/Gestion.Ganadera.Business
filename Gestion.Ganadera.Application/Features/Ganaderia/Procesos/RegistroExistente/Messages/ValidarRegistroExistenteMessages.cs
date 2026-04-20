namespace Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Messages;

public static class ValidarRegistroExistenteMessages
{
    public const string FincaNoExiste = "La finca indicada no existe.";
    public const string PotreroNoExiste = "El potrero indicado no existe.";
    public const string PotreroNoPerteneceAFinca = "El potrero indicado no pertenece a la finca seleccionada.";
    public const string CategoriaNoExiste = "La categoría indicada no existe.";
    public const string RangoNoExiste = "El rango de edad indicado no existe.";
    public const string TipoIdentificadorNoExiste = "El tipo de identificador indicado no existe.";
    public const string IdentificadorDuplicado = "Ya existe un animal activo con este identificador en la base de datos.";
    public const string SexoInvalido = "El formato del sexo es inválido.";
    public const string IdentificadorRequerido = "El identificador principal es obligatorio.";
    public const string IdentificadorFormatoInvalido = "El formato del identificador es inválido.";
    public const string FincaCodigoInvalido = "El código de la finca debe ser mayor a 0.";
    public const string PotreroCodigoInvalido = "El código del potrero debe ser mayor a 0.";
    public const string CategoriaCodigoInvalido = "El código de la categoría debe ser mayor a 0.";
    public const string RangoEdadCodigoInvalido = "El código del rango de edad debe ser mayor a 0.";
    public const string TipoIdentificadorCodigoInvalido = "El código del tipo de identificador debe ser mayor a 0.";
    public const string SexoRequerido = "El sexo del animal es obligatorio.";
    public const string FechaInformadaRequerida = "La fecha del evento es obligatoria.";
    public const string FechaInformadaFutura = "La fecha del evento no puede ser mayor a la fecha actual.";
}
