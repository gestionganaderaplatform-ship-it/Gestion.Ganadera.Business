namespace Gestion.Ganadera.Business.Application.Features.Base.Models
{
    /// <summary>
    /// Request usado para validar si una propiedad existe y puede usarse en operaciones genericas.
    /// </summary>
    public class PropiedadExistenteRequest
    {
        public string Entidad { get; set; } = string.Empty;
        public string NombrePropiedad { get; set; } = string.Empty;
    }
}

