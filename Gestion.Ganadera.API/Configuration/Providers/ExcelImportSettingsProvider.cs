using Microsoft.Extensions.Options;
using Gestion.Ganadera.API.Options;
using Gestion.Ganadera.Application.Abstractions.Interfaces;

namespace Gestion.Ganadera.API.Configuration.Providers
{
    /// <summary>
    /// Traduce configuracion del host a un contrato reusable para importaciones Excel.
    /// </summary>
    public class ExcelImportSettingsProvider(IOptions<ExcelImportOptions> options)
        : IExcelImportSettingsProvider
    {
        private readonly ExcelImportOptions _options = options.Value;

        public int MaxRowsPerImport => _options.MaxRowsPerImport;
    }
}
