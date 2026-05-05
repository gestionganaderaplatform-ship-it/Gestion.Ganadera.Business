using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

/// <summary>
/// Detalle especifico para el evento de cambio de categoria.
/// Registra el salto entre la categoria anterior y la nueva.
/// </summary>
public class EventoDetalleCambioCategoria : AuditableEntity
{
    /// <summary>
    /// PK y FK hacia Evento_Ganadero (Relacion 1:1)
    /// </summary>
    public long Evento_Ganadero_Codigo { get; set; }
    
    public long Categoria_Anterior_Codigo { get; set; }
    
    public long Categoria_Nueva_Codigo { get; set; }
    
    public decimal? Evento_Detalle_Cambio_Categoria_Peso_Al_Cambio { get; set; }
    
    public string? Evento_Detalle_Cambio_Categoria_Observacion { get; set; }

    // Propiedades de navegacion
    public virtual EventoGanadero? EventoGanadero { get; set; }
    public virtual CategoriaAnimal? CategoriaAnterior { get; set; }
    public virtual CategoriaAnimal? CategoriaNueva { get; set; }
}
