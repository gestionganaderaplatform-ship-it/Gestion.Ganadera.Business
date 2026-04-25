namespace Gestion.Ganadera.Business.API.Options
{
    /// <summary>
    /// Opciones operativas que controlan limites y comportamiento de importaciones Excel.
    /// </summary>
    public class ExcelImportOptions
    {
        public const string SectionName = "ExcelImport";
        public int MaxRowsPerImport { get; set; } = 500;
    }
}

