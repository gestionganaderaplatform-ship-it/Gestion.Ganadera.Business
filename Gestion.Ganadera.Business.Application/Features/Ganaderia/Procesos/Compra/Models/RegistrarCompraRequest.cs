namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Models;

public class RegistrarCompraRequest : ValidarCompraRequest
{
    public DateTime Fecha_Compra { get; set; }
    public string Origen_Vendedor { get; set; } = string.Empty;
    public decimal? Valor_Individual { get; set; }
    public string? Observacion { get; set; }
}
