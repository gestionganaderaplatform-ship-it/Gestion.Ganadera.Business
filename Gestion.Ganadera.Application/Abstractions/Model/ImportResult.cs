namespace Gestion.Ganadera.Application.Abstractions.Model
{
    /// <summary>
    /// Resume el resultado de una importacion para que la API lo exponga como respuesta JSON.
    /// </summary>
    public class ImportResult
    {
        public int TotalFilasLeidas { get; set; }
        public int TotalFilasImportadas { get; set; }
        public List<string> Errores { get; set; } = [];
    }
}
