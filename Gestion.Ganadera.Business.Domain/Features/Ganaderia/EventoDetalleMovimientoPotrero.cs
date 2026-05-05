using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class EventoDetalleMovimientoPotrero : AuditableEntity
{
    public long Evento_Ganadero_Codigo { get; set; }
    public DateTime Evento_Detalle_Movimiento_Potrero_Fecha { get; set; }
    public long? Potrero_Codigo_Origen { get; set; }
    public long Potrero_Codigo_Destino { get; set; }
}