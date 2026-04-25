using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class EventoDetalleCompra : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public long Tipo_Identificador_Codigo { get; set; }
    public string Evento_Detalle_Compra_Identificador_Valor { get; set; } = string.Empty;
    public long Categoria_Animal_Codigo { get; set; }
    public long Rango_Edad_Codigo { get; set; }
    public long Potrero_Codigo { get; set; }
    public string Evento_Detalle_Compra_Sexo { get; set; } = string.Empty;
    public DateTime Evento_Detalle_Compra_Fecha_Compra { get; set; }
    public string Evento_Detalle_Compra_Origen_Vendedor { get; set; } = string.Empty;
    public decimal? Evento_Detalle_Compra_Valor_Individual { get; set; }
    public string? Evento_Detalle_Compra_Observacion { get; set; }
}
