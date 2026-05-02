namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.ViewModels;

public class MadreElegibleViewModel
{
    public long Animal_Codigo { get; set; }
    public string Animal_Identificador_Principal { get; set; } = string.Empty;
    public string Categoria_Animal_Nombre { get; set; } = string.Empty;
    public long Potrero_Codigo { get; set; }
    public string Potrero_Nombre { get; set; } = string.Empty;
    public string Animal_Sexo { get; set; } = string.Empty;
}
