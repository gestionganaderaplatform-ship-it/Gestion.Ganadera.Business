namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Messages;

public static class PesajeMessages
{
    public const decimal PesoMinimo = 5m;
    public const decimal PesoMaximo = 1500m;

    public const string AnimalNoEncontrado = "El animal no existe o no está activo.";
    public const string AnimalObligatorio = "El animal es obligatorio.";
    public const string FechaObligatoria = "La fecha del pesaje es obligatoria.";
    public const string PesoMinimoMsg = "El peso debe ser mayor a 5 kg.";
    public const string PesoMaximoMsg = "El peso no puede exceder 1500 kg.";
    public const string PesajeRegistrado = "Pesaje registrado correctamente.";
    public const string FincaObligatoria = "La finca es obligatoria.";
}