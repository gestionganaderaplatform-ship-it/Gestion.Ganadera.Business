namespace Gestion.Ganadera.Business.Application.Abstractions.Interfaces
{
    /// <summary>
    /// Expone la configuracion transversal para importaciones masivas desde Excel.
    /// </summary>
    public interface IExcelImportSettingsProvider
    {
        int MaxRowsPerImport { get; }
    }
}
