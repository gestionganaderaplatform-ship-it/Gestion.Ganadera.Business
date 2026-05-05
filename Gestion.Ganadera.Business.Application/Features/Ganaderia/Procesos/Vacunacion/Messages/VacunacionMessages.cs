namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Messages;

public static class VacunacionMessages
{
public const string FincaObligatoria = "Debe seleccionar una finca.";
    public const string FincaInvalida = "La finca no existe o no está activa.";
    public const string AnimalesObligatorios = "Debe seleccionar al menos un animal.";
    public const string AnimalesInvalidos = "Alguno(s) animal(es) no existe(n) o está(n) inactivo(s).";
    public const string VacunaObligatoria = "Debe seleccionar una vacuna.";
    public const string VacunaInvalida = "La vacuna no existe o no está activa.";
    public const string CicloObligatorio = "El ciclo de vacunación es obligatorio.";
    public const string FechaObligatoria = "La fecha de aplicación es obligatoria.";
    public const string FechaFutura = "La fecha no puede ser futura.";
    public const string LoteLongitudMaxima = "El lote del biológico excede los 50 caracteres.";
    public const string LoteObligatorio = "El lote del biológico es obligatorio.";
    public const string SoporteObligatorio = "Debe adjuntar el soporte o certificado de la vacunación.";
    public const string EnfermedadObligatoria = "La enfermedad objetivo es obligatoria.";
    public const string EnfermedadInvalida = "La enfermedad no existe o no está activa.";
    public const string VacunacionRegistrada = "Vacunación registrada correctamente.";
    public const string VacunacionLoteRegistrada = "Vacunación registrada para {0} animales.";
    public const string AnimalInactivo = "No puede vacuna un animal inactivo.";
    public const string AnimalNoEncontrado = "Animal no encontrado en el sistema.";
}