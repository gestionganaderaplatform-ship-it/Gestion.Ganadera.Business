namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.ViewModels;

public class InicioDashboardViewModel
{
    public int Animales_Activos { get; set; }
    public int Potreros_Con_Animales { get; set; }
    public int Ingresos_Ultimos_30_Dias { get; set; }
    public int Eventos_Ultimos_7_Dias { get; set; }
    public IEnumerable<string> Lecturas_Operativas { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<InicioDashboardDistribucionViewModel> Distribucion_Categorias { get; set; } =
        Enumerable.Empty<InicioDashboardDistribucionViewModel>();
    public IEnumerable<InicioDashboardDistribucionViewModel> Distribucion_Potreros { get; set; } =
        Enumerable.Empty<InicioDashboardDistribucionViewModel>();
    public IEnumerable<InicioDashboardActividadViewModel> Actividad_Reciente { get; set; } =
        Enumerable.Empty<InicioDashboardActividadViewModel>();
}

public class InicioDashboardDistribucionViewModel
{
    public string Nombre { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal Porcentaje { get; set; }
}

public class InicioDashboardActividadViewModel
{
    public long Evento_Ganadero_Codigo { get; set; }
    public string Evento_Ganadero_Tipo { get; set; } = string.Empty;
    public DateTime Evento_Ganadero_Fecha { get; set; }
    public string Animal_Identificador_Principal { get; set; } = string.Empty;
    public string Potrero_Nombre { get; set; } = string.Empty;
}
