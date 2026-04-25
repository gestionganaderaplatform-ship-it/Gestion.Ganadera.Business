namespace Gestion.Ganadera.Business.Domain.Features.Navegacion
{
    /// <summary>
    /// Define los nodos jerarquicos de navegacion que consume el shell del Web.
    /// </summary>
    public class MenuNavegacion
    {
        public long Menu_Navegacion_Codigo { get; set; }
        public long? Menu_Navegacion_Padre_Codigo { get; set; }
        public string Menu_Navegacion_Clave { get; set; } = string.Empty;
        public string Menu_Navegacion_Titulo { get; set; } = string.Empty;
        public string Menu_Navegacion_Icono { get; set; } = string.Empty;
        public string Menu_Navegacion_Tipo { get; set; } = string.Empty;
        public string? Menu_Navegacion_Ruta { get; set; }
        public string? Menu_Navegacion_Accion { get; set; }
        public int Menu_Navegacion_Orden { get; set; }
        public bool Menu_Navegacion_Esta_Activo { get; set; }
        public bool Menu_Navegacion_Requiere_Cuenta_Padre { get; set; }
        public string? Menu_Navegacion_Permiso_Requerido { get; set; }
    }
}
