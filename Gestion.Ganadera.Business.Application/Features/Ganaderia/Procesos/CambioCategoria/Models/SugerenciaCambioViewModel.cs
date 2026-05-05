namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.CambioCategoria.Models;

public class SugerenciaCambioViewModel
{
    public long Sugerencia_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public string Animal_Identificador { get; set; } = string.Empty;
    public string Animal_Nombre { get; set; } = string.Empty;
    
    public long Categoria_Actual_Codigo { get; set; }
    public string Categoria_Actual_Nombre { get; set; } = string.Empty;
    
    public long Categoria_Sugerida_Codigo { get; set; }
    public string Categoria_Sugerida_Nombre { get; set; } = string.Empty;
    
    public string Motivo { get; set; } = string.Empty;
    public int Edad_Meses_Aproximada { get; set; }
    public decimal? Ultimo_Peso { get; set; }
    public DateTime Fecha_Sugerencia { get; set; }
}
