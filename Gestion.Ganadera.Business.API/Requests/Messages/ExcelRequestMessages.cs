namespace Gestion.Ganadera.Business.API.Requests.Messages
{
    /// <summary>
    /// Reune mensajes comunes para endpoints de importacion y exportacion Excel.
    /// </summary>
    public static class ExcelRequestMessages
    {
        public const string FileRequired = "Debes adjuntar un archivo Excel.";
        public const string FileMustBeXlsx = "Solo se permiten archivos Excel con extension .xlsx.";
        public const string NoRowsImported = "No se pudo importar ninguna fila del archivo.";
    }
}
