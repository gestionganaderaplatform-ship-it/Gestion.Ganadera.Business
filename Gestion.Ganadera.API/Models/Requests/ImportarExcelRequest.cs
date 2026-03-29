using Microsoft.AspNetCore.Http;

namespace Gestion.Ganadera.API.Models.Requests
{
    /// <summary>
    /// Request base para uploads simples por Swagger o clientes HTTP usando multipart/form-data.
    /// </summary>
    public class ImportarExcelRequest
    {
        public IFormFile Archivo { get; set; } = null!;
    }
}
