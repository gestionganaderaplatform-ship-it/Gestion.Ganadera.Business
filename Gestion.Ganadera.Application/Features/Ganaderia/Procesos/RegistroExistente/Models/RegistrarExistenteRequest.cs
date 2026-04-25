namespace Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;

public class RegistrarExistenteRequest : ValidarRegistroExistenteRequest
{
    public DateTime Fecha_Informada { get; set; }
    public string? Observacion { get; set; }
}

public class IdentificadorIndividualRequest
{
    public string Identificador_Principal { get; set; } = string.Empty;
    public long Tipo_Identificador_Codigo { get; set; }
    public DateTime? Fecha_Nacimiento { get; set; }
}

public class RegistrarExistenteLoteRequest
{
    public long Finca_Codigo { get; set; }
    public long Potrero_Codigo { get; set; }
    public long Categoria_Animal_Codigo { get; set; }
    public long Rango_Edad_Codigo { get; set; }
    public string Animal_Sexo { get; set; } = string.Empty;
    public DateTime? Fecha_Nacimiento_Comun { get; set; }
    public DateTime Fecha_Informada { get; set; }
    public string? Observacion { get; set; }
    
    public List<IdentificadorIndividualRequest> Animales { get; set; } = new();
}
