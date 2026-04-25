namespace Gestion.Ganadera.Business.Application.Features.Base.ViewModels
{
    /// <summary>
    /// Agrupa los codigos y la propiedad clave para consultas masivas de existencia.
    /// </summary>
    public class ExistenVariosRequest<TProperty> where TProperty : class
    {
        public IEnumerable<TProperty> Codigos { get; set; } = new List<TProperty>();
        public string PropiedadClave { get; set; } = string.Empty;
    }
}

