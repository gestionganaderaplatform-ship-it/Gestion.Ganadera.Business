namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Models;

public class RegistrarVentaRequest
{
    public long Finca_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public DateTime Fecha_Venta { get; set; }
    public string Comprador { get; set; } = string.Empty;
    public decimal? Valor { get; set; }
    public string? Observacion { get; set; }
}

public class ValidarVentaRequest
{
    public long Finca_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public DateTime Fecha_Venta { get; set; }
}

public class RegistrarVentaLoteRequest
{
    public long Finca_Codigo { get; set; }
    public DateTime Fecha_Venta { get; set; }
    public string Comprador { get; set; } = string.Empty;
    public decimal? Valor_Total { get; set; }
    public string? Observacion { get; set; }
    public List<AnimalVenta> Animales { get; set; } = [];
}

public class ValidarVentaLoteRequest
{
    public long Finca_Codigo { get; set; }
    public DateTime Fecha_Venta { get; set; }
    public List<AnimalVenta> Animales { get; set; } = [];
}

public class AnimalVenta
{
    public long Animal_Codigo { get; set; }
    public long Finca_Codigo { get; set; }
    public decimal? Valor { get; set; }
}