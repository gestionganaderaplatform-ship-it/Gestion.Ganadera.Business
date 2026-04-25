namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Models;

public class ValidarCompraRequest
{
    public long Finca_Codigo { get; set; }
    public long Potrero_Codigo { get; set; }
    public long Categoria_Animal_Codigo { get; set; }
    public long Rango_Edad_Codigo { get; set; }
    public long Tipo_Identificador_Codigo { get; set; }
    public string Identificador_Principal { get; set; } = string.Empty;
    public string Animal_Sexo { get; set; } = string.Empty;
}
