namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Messages;

public static class ValidarRegistroExistenteMessages
{
    public const string FincaNoExiste = "La finca indicada no existe.";
    public const string PotreroNoExiste = "El potrero indicado no existe.";
    public const string PotreroNoPerteneceAFinca = "El potrero indicado no pertenece a la finca seleccionada.";
    public const string CategoriaNoExiste = "La categoria indicada no existe.";
    public const string RangoNoExiste = "El rango de edad indicado no existe.";
    public const string TipoIdentificadorNoExiste = "El tipo de identificador indicado no existe.";
    public const string IdentificadorDuplicado = "Ya existe un animal activo con este identificador en la base de datos.";
    public const string IdentificadoresRepetidosEnLote = "No se pueden repetir identificadores dentro del lote.";
    public const string SexoInvalido = "El formato del sexo es invalido.";
    public const string IdentificadorRequerido = "El identificador principal es obligatorio.";
    public const string IdentificadorFormatoInvalido = "El formato del identificador es invalido.";
    public const string IdentificadoresConsultaRequeridos = "Debe indicar al menos un identificador para validar.";
    public const string FincaCodigoInvalido = "El codigo de la finca debe ser mayor a 0.";
    public const string PotreroCodigoInvalido = "El codigo del potrero debe ser mayor a 0.";
    public const string CategoriaCodigoInvalido = "El codigo de la categoria debe ser mayor a 0.";
    public const string RangoEdadCodigoInvalido = "El codigo del rango de edad debe ser mayor a 0.";
    public const string TipoIdentificadorCodigoInvalido = "El codigo del tipo de identificador debe ser mayor a 0.";
    public const string SexoRequerido = "El sexo del animal es obligatorio.";
    public const string FechaInformadaRequerida = "La fecha del evento es obligatoria.";
    public const string FechaInformadaFutura = "La fecha del evento no puede ser mayor a la fecha actual.";
    public const string FechaNacimientoFutura = "La fecha de nacimiento no puede ser futura.";
    public const string CategoriaIncompatibleConSexo = "La categoria indicada no es compatible con el sexo del animal.";
    public const string LoteSinAnimales = "Debe incluir al menos un animal en el lote.";
    public const string LoteSuperaMaximo = "El lote no puede superar los 100 animales por transaccion.";
    public const string IdentificadorLongitudMaxima = "El identificador principal no puede superar los 50 caracteres.";
}
