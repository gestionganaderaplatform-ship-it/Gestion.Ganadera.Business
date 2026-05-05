namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Messages;

public static class MuerteMessages
{
    public const string AnimalNoEncontrado = "El animal no existe.";
    public const string AnimalObligatorio = "El animal es obligatorio.";
    public const string AnimalInactivo = "El animal no está activo.";
    public const string AnimalYaMuerto = "El animal ya fue reportado como muerto.";
    public const string FechaMuerteRequerida = "La fecha de muerte es obligatoria.";
    public const string CausaRequerida = "Debe registrar una causa o motivo.";
    public const string MuerteRegistrada = "Muerte registrada correctamente.";
    public const string FechaFutura = "La fecha de muerte no puede ser futura.";
    public const string FechaMuyAntigua = "La fecha de muerte no puede ser superior a 7 días en el pasado.";
    public const string FincaRequerida = "La finca es obligatoria.";
}