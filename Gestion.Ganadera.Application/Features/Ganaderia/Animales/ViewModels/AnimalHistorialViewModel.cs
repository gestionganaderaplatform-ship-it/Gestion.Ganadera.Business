namespace Gestion.Ganadera.Application.Features.Ganaderia.Animales.ViewModels;

public class AnimalHistorialViewModel
{
    public long Evento_Ganadero_Animal_Codigo { get; set; }
    public long Evento_Ganadero_Codigo { get; set; }
    public string Evento_Ganadero_Tipo { get; set; } = string.Empty;
    public DateTime Evento_Ganadero_Fecha { get; set; }
    public string Evento_Ganadero_Animal_Estado_Afectacion { get; set; } = string.Empty;
    public string Evento_Ganadero_Registrado_Por { get; set; } = string.Empty;
    public string? Evento_Ganadero_Observacion { get; set; }
    public bool Evento_Ganadero_Es_Correccion { get; set; }
    public bool Evento_Ganadero_Es_Anulacion { get; set; }
}
