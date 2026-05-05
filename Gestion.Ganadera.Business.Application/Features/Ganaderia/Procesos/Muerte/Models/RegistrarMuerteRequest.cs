namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Models;

public class RegistrarMuerteRequest
{
    public long Finca_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public DateTime Fecha_Muerte { get; set; }
    public long Causa_Muerte_Codigo { get; set; }
    public string? Observacion { get; set; }
}

public class ValidarMuerteRequest
{
    public long Finca_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public DateTime Fecha_Muerte { get; set; }
    public long Causa_Muerte_Codigo { get; set; }
}

public class RegistrarMuerteLoteRequest
{
    public long Finca_Codigo { get; set; }
    public List<long> Animales_Codigos { get; set; } = [];
    public DateTime Fecha_Muerte { get; set; }
    public long Causa_Muerte_Codigo { get; set; }
    public string? Observacion { get; set; }
}

public class ValidarMuerteLoteRequest
{
    public long Finca_Codigo { get; set; }
    public List<long> Animales_Codigos { get; set; } = [];
    public DateTime Fecha_Muerte { get; set; }
    public long Causa_Muerte_Codigo { get; set; }
}