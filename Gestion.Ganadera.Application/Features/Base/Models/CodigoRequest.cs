namespace Gestion.Ganadera.Application.Features.Base.Models
{
    /// <summary>
    /// Request minimo para operaciones que reciben un codigo numerico por entrada externa.
    /// </summary>
    public class CodigoRequest
    {
        public string Codigo { get; set; } = string.Empty;
    }
}

