namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public static class AnimalOrigenIngreso
{
    public const string RegistroExistente = "REGISTRO_EXISTENTE";
    public const string Compra = "COMPRA";
}

public static class EventoGanaderoTipo
{
    public const string RegistroExistente = "REGISTRO_EXISTENTE";
    public const string Compra = "COMPRA";
}

public static class EventoGanaderoEstado
{
    public const string Completado = "COMPLETADO";
}

public static class EventoGanaderoAnimalEstadoAfectacion
{
    public const string Procesado = "PROCESADO";
}
