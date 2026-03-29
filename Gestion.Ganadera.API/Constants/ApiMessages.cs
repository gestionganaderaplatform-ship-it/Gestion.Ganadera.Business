using Gestion.Ganadera.API.ErrorHandling.Messages;

namespace Gestion.Ganadera.API.Constants
{
    /// <summary>
    /// Mantiene acceso central a mensajes HTTP comunes del template.
    /// </summary>
    public static class ApiMessages
    {
        public const string ValidationFailedTitle = ApiErrorMessages.ValidationFailedTitle;
        public const string ValidationFailedDetail = ApiErrorMessages.ValidationFailedDetail;
        public const string NotFoundTitle = ApiErrorMessages.NotFoundTitle;
        public const string InternalErrorTitle = ApiErrorMessages.InternalErrorTitle;
        public const string InvalidNumericCodes = ApiErrorMessages.InvalidNumericCodes;
        public const string OperationFailed = ApiErrorMessages.OperationFailed;
    }
}
