namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public static class AnimalOrigenIngreso
{
    public const string RegistroExistente = "REGISTRO_EXISTENTE";
    public const string Compra = "COMPRA";
    public const string Nacimiento = "NACIMIENTO";
}

public static class EventoGanaderoTipo
{
    public const string RegistroExistente = "REGISTRO_EXISTENTE";
    public const string Compra = "COMPRA";
    public const string Nacimiento = "NACIMIENTO";
    public const string Pesaje = "PESAJE";
    public const string Muerte = "MUERTE";
    public const string Venta = "VENTA";
    public const string MovimientoPotrero = "MOVIMIENTO_POTRERO";
    public const string Vacunacion = "VACUNACION";
    public const string TrasladoFinca = "TRASLADO_FINCA";
    public const string TratamientoSanitario = "TRATAMIENTO_SANITARIO";
    public const string RevisionReproductiva = "REVISION_REPRODUCTIVA";
    public const string Destete = "DESTETE";
    public const string Descarte = "DESCARTE";
    public const string CambioCategoria = "CAMBIO_CATEGORIA";
}

public static class EventoGanaderoEstado
{
    public const string Completado = "COMPLETADO";
}

public static class EventoGanaderoAnimalEstadoAfectacion
{
    public const string Procesado = "PROCESADO";
}

public static class AnimalRelacionFamiliarTipo
{
    public const string MadreCria = "MADRE_CRIA";
}

public static class VacunaCiclo
{
    public const string PrimeraDosis = "PRIMERA_DOSIS";
    public const string Refuerzo = "REFUERZO";
    public const string Refuerzo2 = "REFUERZO_2";
    public const string Revacunacion = "REVACUNACION";
}

public static class CambioCategoriaSugerenciaEstado
{
    public const string Pendiente = "PENDIENTE";
    public const string Aprobado = "APROBADO";
    public const string Rechazado = "RECHAZADO";
    public const string Omitido = "OMITIDO";
}

public static class CambioCategoriaSugerenciaMotivo
{
    public const string EdadPermanencia = "EDAD_PERMANENCIA";
    public const string Peso = "PESO";
    public const string EventoFisiologico = "EVENTO_FISIOLOGICO";
}
