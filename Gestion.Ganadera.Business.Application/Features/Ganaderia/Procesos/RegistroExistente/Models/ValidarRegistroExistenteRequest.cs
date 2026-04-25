namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;

public class ValidarRegistroExistenteRequest
{
    public string Identificador_Principal { get; set; } = string.Empty;
    public long Tipo_Identificador_Codigo { get; set; }
    public long Finca_Codigo { get; set; }
    public long Potrero_Codigo { get; set; }
    public long Categoria_Animal_Codigo { get; set; }
    public long Rango_Edad_Codigo { get; set; }
    public string Animal_Sexo { get; set; } = string.Empty;
    public DateTime? Fecha_Nacimiento { get; set; }
}
