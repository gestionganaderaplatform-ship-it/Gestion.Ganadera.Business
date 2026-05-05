namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Messages;

public static class DescarteMessages
{
    public const string FincaRequerida = "La finca es obligatoria.";
    public const string AnimalRequerido = "El animal es obligatorio.";
    public const string MotivoRequerido = "El motivo de descarte es obligatorio.";
    public const string FechaRequerida = "La fecha de descarte es obligatoria.";
    public const string FechaInvalida = "La fecha de descarte no puede ser futura.";
    public const string AnimalesRequeridos = "Debe seleccionar al menos un animal.";
    
    public const string AnimalNoEncontrado = "El animal no existe en la finca activa.";
    public const string AnimalInactivo = "El animal ya se encuentra inactivo.";
    public const string AnimalYaDescartado = "El animal ya ha sido descartado previamente.";

    public const string RegistroFallido = "No fue posible registrar el descarte. Verifique que el animal esté activo.";
    public const string RegistroLoteFallido = "Ocurrió un error al procesar el lote de descartes.";
}
