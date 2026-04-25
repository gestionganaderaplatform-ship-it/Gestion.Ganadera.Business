namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.ViewModels;

public class AnimalViewModel
{
    public long Animal_Codigo { get; set; }
    public string Animal_Identificador_Principal { get; set; } = string.Empty;
    public string Animal_Sexo { get; set; } = string.Empty;
    public string Categoria_Animal_Nombre { get; set; } = string.Empty;
    public string Finca_Nombre { get; set; } = string.Empty;
    public string Potrero_Nombre { get; set; } = string.Empty;
    public bool Animal_Activo { get; set; }

    public string Animal_Origen_Ingreso { get; set; } = string.Empty;
    public DateTime Animal_Fecha_Ingreso_Inicial { get; set; }
    public DateTime Animal_Fecha_Registro_Ingreso { get; set; }
    public DateTime Animal_Fecha_Ultimo_Evento { get; set; }
}
