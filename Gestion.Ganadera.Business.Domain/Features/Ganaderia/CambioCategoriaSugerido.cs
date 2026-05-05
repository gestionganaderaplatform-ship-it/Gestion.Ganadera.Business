using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

/// <summary>
/// Representa una sugerencia generada por el sistema para cambiar la categoria de un animal
/// basada en criterios cronologicos (edad) o productivos (peso).
/// </summary>
public class CambioCategoriaSugerido : AuditableEntity
{
    public long Cambio_Categoria_Sugerido_Codigo { get; set; }
    
    public long Animal_Codigo { get; set; }
    
    public long Categoria_Actual_Codigo { get; set; }
    
    public long Categoria_Sugerida_Codigo { get; set; }
    
    /// <summary>
    /// Motivo de la sugerencia: Edad, Peso, EventoFisiologico, etc.
    /// </summary>
    public string Sugerencia_Motivo { get; set; } = string.Empty;
    
    /// <summary>
    /// Estado de la sugerencia: Pendiente, Aprobado, Rechazado, Omitido.
    /// </summary>
    public string Sugerencia_Estado { get; set; } = CambioCategoriaSugerenciaEstado.Pendiente;

    public DateTime Fecha_Sugerencia { get; set; } = DateTime.Now;

    // Propiedades de navegacion
    public virtual Animal? Animal { get; set; }
    public virtual CategoriaAnimal? CategoriaActual { get; set; }
    public virtual CategoriaAnimal? CategoriaSugerida { get; set; }
}
