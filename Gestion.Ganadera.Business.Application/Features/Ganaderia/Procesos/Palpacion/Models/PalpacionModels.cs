namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Models;

public class RegistrarPalpacionRequest
{
    public long Animal_Codigo { get; set; }
    public long Palpacion_Resultado_Codigo { get; set; }
    public DateTime Fecha_Revision { get; set; }
    public string? Responsable { get; set; }
    public string? Dato_Complementario { get; set; }
    public string? Observacion { get; set; }
}

public class ValidarPalpacionRequest
{
    public long Animal_Codigo { get; set; }
    public long Palpacion_Resultado_Codigo { get; set; }
    public DateTime Fecha_Revision { get; set; }
}

public class RegistrarPalpacionLoteRequest
{
    public List<long> Animales { get; set; } = [];
    public long Palpacion_Resultado_Codigo { get; set; }
    public DateTime Fecha_Revision { get; set; }
    public string? Responsable { get; set; }
    public string? Dato_Complementario { get; set; }
    public string? Observacion { get; set; }
}

public class ValidarPalpacionLoteRequest
{
    public List<long> Animales { get; set; } = [];
    public long Palpacion_Resultado_Codigo { get; set; }
    public DateTime Fecha_Revision { get; set; }
}
