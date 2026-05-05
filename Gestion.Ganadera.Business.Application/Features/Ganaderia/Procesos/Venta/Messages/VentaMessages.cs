namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Messages;

public static class VentaMessages
{
    public const string AnimalNoEncontrado = "El animal no existe.";
    public const string AnimalInactivo = "El animal no está activo.";
    public const string AnimalYaVendido = "El animal ya fue vendido.";
    public const string FechaVentaRequerida = "La fecha de venta es obligatoria.";
    public const string CompradorRequerido = "Debe registrar el comprador o destino.";
    public const string VentaRegistrada = "Venta registrada correctamente.";
    public const string ValorRequerido = "El valor de la venta debe ser mayor a cero.";
    public const string FechaFutura = "La fecha de venta no puede ser futura.";
    public const string AnimalRequerido = "El animal es obligatorio.";
    public const string AlMenosUnAnimal = "Debe incluir al menos un animal.";
    public const string FincaRequerida = "La finca es obligatoria.";
}