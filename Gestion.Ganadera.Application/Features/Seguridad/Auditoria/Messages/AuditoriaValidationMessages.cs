namespace Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Messages
{
    /// <summary>
    /// Reune mensajes de validacion propios de la feature de auditoria.
    /// </summary>
    public static class AuditoriaValidationMessages
    {
        public const string AuditNotFound = "El codigo de la auditoria proporcionado no existe.";
        public const string ModifiedDateCannotBeFuture = "La fecha de modificacion no puede ser en el futuro.";
        public const string ActorIdentifierInvalidFormat = "El usuario contiene caracteres no permitidos.";
        public const string TableNameInvalidFormat = "La tabla contiene caracteres no permitidos.";
        public const string InvalidModifiedDateRange = "La fecha final no puede ser menor a la fecha inicial.";
        public const string QueryDateRangeRequired = "Define fecha inicio y fecha fin para consultar la auditoria.";
        public const string QueryDateRangeIncomplete = "Completa fecha inicio y fecha fin para consultar la auditoria.";
        public const string ExportDateFromRequired = "La fecha inicial es obligatoria para exportar auditoria.";
        public const string ExportDateToRequired = "La fecha final es obligatoria para exportar auditoria.";
        public const string ExportDateRangeExceeded = "El rango maximo permitido para exportar es de 90 dias.";
        public const string ExportNoDataFound = "No existen datos de auditoria para exportar con el rango seleccionado.";

        public static string ActorIdentifierTooLong(int maxCaracteres)
            => $"El usuario no puede superar los {maxCaracteres} caracteres.";

        public static string TableNameTooLong(int maxCaracteres)
            => $"La tabla no puede superar los {maxCaracteres} caracteres.";

        public static string ExportRecordLimitExceeded(int maxRegistros)
            => $"La exportacion supera el limite permitido de {maxRegistros} registros. Ajusta el rango de fechas o agrega mas filtros.";
    }
}
