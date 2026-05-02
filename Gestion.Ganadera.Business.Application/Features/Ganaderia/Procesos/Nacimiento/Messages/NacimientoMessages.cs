namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Messages;

public static class NacimientoMessages
{
    public const string FincaNoExiste = "La finca indicada no existe.";
    public const string MadreNoExiste = "La madre indicada no existe o no pertenece a la finca seleccionada.";
    public const string MadreNoElegible = "Selecciona una vaca o novilla activa de la finca para registrar el nacimiento.";
    public const string PotreroNoExiste = "El potrero indicado no existe.";
    public const string PotreroNoPerteneceAFinca = "El potrero indicado no pertenece a la finca seleccionada.";
    public const string CategoriaNoExiste = "La categoría indicada no existe.";
    public const string TipoIdentificadorNoExiste = "El tipo de identificador indicado no existe.";
    public const string IdentificadorDuplicado = "Ya existe un animal activo con este identificador en la base de datos.";
    public const string SexoInvalido = "El formato del sexo es inválido.";
    public const string IdentificadorRequerido = "El identificador principal es obligatorio.";
    public const string IdentificadorFormatoInvalido = "El formato del identificador es inválido.";
    public const string FincaCodigoInvalido = "El código de la finca debe ser mayor a 0.";
    public const string MadreCodigoInvalido = "El código de la madre debe ser mayor a 0.";
    public const string PotreroCodigoInvalido = "El código del potrero debe ser mayor a 0.";
    public const string CategoriaCodigoInvalido = "El código de la categoría debe ser mayor a 0.";
    public const string TipoIdentificadorCodigoInvalido = "El código del tipo de identificador debe ser mayor a 0.";
    public const string SexoRequerido = "El sexo de la cría es obligatorio.";
    public const string FechaNacimientoRequerida = "La fecha de nacimiento es obligatoria.";
    public const string FechaNacimientoFutura = "La fecha de nacimiento no puede ser mayor a la fecha actual.";
    public const string CategoriaIncompatibleConSexo = "La categoría indicada no es compatible con el sexo de la cría.";
    public const string TipoIdentificadorInternoNoDisponible = "No existe el tipo de identificador interno del sistema para este cliente.";
    public const string PesoNacerInvalido = "El peso al nacer debe ser mayor a 0.";
}
