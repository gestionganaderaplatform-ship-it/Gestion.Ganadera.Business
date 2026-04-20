using Gestion.Ganadera.Domain.Base;

namespace Gestion.Ganadera.Domain.Features.Ganaderia;

public class EventoDetalleRegistroExistente : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public long Tipo_Identificador_Codigo { get; set; }
    public string Evento_Detalle_Registro_Existente_Identificador_Valor { get; set; } = string.Empty;
    public long Categoria_Animal_Codigo { get; set; }
    public long Rango_Edad_Codigo { get; set; }
    public long Potrero_Codigo { get; set; }
    public string Evento_Detalle_Registro_Existente_Sexo { get; set; } = string.Empty;
    public DateTime? Evento_Detalle_Registro_Existente_Fecha_Informada { get; set; }
}
