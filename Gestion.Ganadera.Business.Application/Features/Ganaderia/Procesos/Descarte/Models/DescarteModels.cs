using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Models;

public class RegistrarDescarteRequest
{
    public long Finca_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public long Descarte_Motivo_Codigo { get; set; }
    public DateTime Evento_Detalle_Descarte_Fecha { get; set; }
    public string? Evento_Detalle_Descarte_Destino { get; set; }
    public decimal? Evento_Detalle_Descarte_Valor { get; set; }
    public string? Evento_Detalle_Descarte_Observacion { get; set; }
}

public class ValidarDescarteRequest
{
    public long Finca_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public long Descarte_Motivo_Codigo { get; set; }
    public DateTime Evento_Detalle_Descarte_Fecha { get; set; }
}

public class RegistrarDescarteLoteRequest
{
    public long Finca_Codigo { get; set; }
    public List<long> Animales_Codigos { get; set; } = [];
    public long Descarte_Motivo_Codigo { get; set; }
    public DateTime Evento_Detalle_Descarte_Fecha { get; set; }
    public string? Evento_Detalle_Descarte_Destino { get; set; }
    public decimal? Evento_Detalle_Descarte_Valor { get; set; }
    public string? Evento_Detalle_Descarte_Observacion { get; set; }
}

public class ValidarDescarteLoteRequest
{
    public long Finca_Codigo { get; set; }
    public List<long> Animales_Codigos { get; set; } = [];
    public long Descarte_Motivo_Codigo { get; set; }
    public DateTime Evento_Detalle_Descarte_Fecha { get; set; }
}
