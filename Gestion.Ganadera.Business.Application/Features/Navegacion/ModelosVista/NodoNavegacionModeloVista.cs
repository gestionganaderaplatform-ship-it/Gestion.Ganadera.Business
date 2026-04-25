namespace Gestion.Ganadera.Business.Application.Features.Navegacion.ModelosVista
{
    /// <summary>
    /// Representa el nodo de navegacion serializado para el shell del Web.
    /// </summary>
    public class NodoNavegacionModeloVista
    {
        public string Clave { get; set; } = string.Empty;

        public string Titulo { get; set; } = string.Empty;

        public string Icono { get; set; } = string.Empty;

        public string Tipo { get; set; } = string.Empty;

        public string? Ruta { get; set; }

        public string? Accion { get; set; }

        public List<NodoNavegacionModeloVista> Hijos { get; set; } = [];
    }
}
