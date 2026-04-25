namespace Gestion.Ganadera.Business.API.ErrorHandling.Messages
{
    /// <summary>
    /// Centraliza textos usados en ProblemDetails y respuestas HTTP comunes.
    /// </summary>
    public static class ApiErrorMessages
    {
        public const string ValidationFailedTitle = "Validacion fallida";
        public const string ValidationFailedDetail = "Existen errores de validacion.";
        public const string NotFoundTitle = "Recurso no encontrado";
        public const string InternalErrorTitle = "Error interno del servidor";
        public const string InvalidJsonTitle = "El cuerpo de la solicitud no es un JSON valido.";
        public const string UnauthorizedTitle = "No autenticado";
        public const string ForbiddenTitle = "Acceso denegado";
        public const string UnauthorizedDetailTitle = "No autorizado";
        public const string RateLimitExceededTitle = "Demasiadas solicitudes";
        public const string UnexpectedErrorDetail = "Ocurrio un error inesperado.";
        public const string RateLimitExceededDetail = "Ha excedido el limite de solicitudes permitidas. Intente nuevamente mas tarde.";
        public const string RequestedRecordNotFound = "No se encontro el registro solicitado.";
        public const string NoRecordsForCriteria = "No se encontraron registros para los criterios enviados.";
        public const string OperationFailed = "No fue posible completar la operacion solicitada.";
        public const string InvalidNumericCodes = "Los codigos deben ser valores numericos validos.";

        public static string RecordNotFound(long code)
            => $"No se encontro el registro con el codigo {code}.";

        public static string FilterNotFound(string filtersText)
            => $"No se encontro el registro con el filtro ({filtersText}).";
    }
}
