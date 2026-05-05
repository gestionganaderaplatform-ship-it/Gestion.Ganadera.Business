namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Messages;

public static class VacunacionMessages
{
    public const string VacunaNombreFormatoInvalido = "El nombre solo puede contener letras, números, espacios y signos de puntuación.";
    public const string VacunaNombreNoDebeEmpezarOTerminarConEspacios = "El nombre no debe empezar o terminar con espacios.";
    public const string VacunaNombreDuplicado = "Ya existe una vacuna con ese nombre.";
    public const string VacunaCodigoInvalido = "El código de vacuna es inválido.";
    public const string VacunaNoExiste = "La vacuna no existe.";
    public const string VacunaInactiva = "La vacuna está inactiva.";
}