namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Messages;

public static class CompraMessages
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
    public const string FechaCompraRequerida = "La fecha de compra es obligatoria.";
    public const string FechaCompraFutura = "La fecha de compra no puede ser mayor a la fecha actual.";
    public const string OrigenVendedorRequerido = "El origen o vendedor es obligatorio.";
    public const string CategoriaIncompatibleConSexo = "La categoría indicada no es compatible con el sexo del animal.";
    public const string LoteSinAnimales = "La compra debe incluir al menos un animal.";
    public const string LoteSuperaMaximo = "La compra no puede incluir más de 100 animales.";
    public const string IdentificadoresRepetidosEnLote = "La compra contiene identificadores repetidos.";
    public const string ValorTotalInvalido = "El valor total de la compra debe ser mayor a 0.";
    public const string ValorIndividualInvalido = "El valor individual del animal debe ser mayor a 0.";
    public const string ValoresIndividualesIncompletos = "Si informas el valor individual de un animal, debes informar el valor individual de todos los animales de la compra.";
    public const string ValorTotalNoCoincide = "El valor total de la compra debe coincidir con la suma de valores individuales.";
}
