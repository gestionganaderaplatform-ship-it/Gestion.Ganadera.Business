namespace Gestion.Ganadera.Application.Abstractions.Model
{
    /// <summary>
    /// Representa un archivo generado por la aplicacion sin acoplar el caso de uso a HTTP.
    /// </summary>
    public class ExcelFileResult
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } =
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public byte[] Content { get; set; } = [];
    }
}
