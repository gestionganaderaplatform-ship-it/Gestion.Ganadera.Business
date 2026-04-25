namespace Gestion.Ganadera.Application.Features.Ganaderia.Animales.ViewModels;

public class GanadoViewModel
{
    public long Animal_Codigo { get; set; }
    public string Animal_Identificador_Principal { get; set; } = string.Empty;
    public string Categoria_Animal_Nombre { get; set; } = string.Empty;
    public string Potrero_Nombre { get; set; } = string.Empty;
    public string Animal_Origen_Ingreso { get; set; } = string.Empty;
    public DateTime Animal_Fecha_Ingreso_Inicial { get; set; }
    public bool Animal_Activo { get; set; }
}
