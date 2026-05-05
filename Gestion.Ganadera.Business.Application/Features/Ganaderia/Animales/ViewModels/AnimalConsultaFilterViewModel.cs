namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.ViewModels;

public class AnimalConsultaFilterViewModel
{
    public string? Animal_Identificador_Principal { get; set; }
    public string? Categoria_Animal_Nombre { get; set; }
    public string? Potrero_Nombre { get; set; }
    public long? Potrero_Codigo { get; set; }
    public DateTime? Animal_Fecha_Ingreso_Inicial { get; set; }
}
