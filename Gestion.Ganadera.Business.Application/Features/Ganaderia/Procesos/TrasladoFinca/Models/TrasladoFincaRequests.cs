namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Models;

public class ValidarTrasladoFincaRequest
{
    public long Finca_Origen_Codigo { get; set; }
    public long Finca_Destino_Codigo { get; set; }
    public long Potrero_Destino_Codigo { get; set; }
    public List<long> Animales_Codigos { get; set; } = [];
}

public class ValidarTrasladoFincaLoteRequest
{
    public long Finca_Origen_Codigo { get; set; }
    public long Finca_Destino_Codigo { get; set; }
    public long Potrero_Destino_Codigo { get; set; }
    public DateTime Fecha_Traslado { get; set; }
    public List<TrasladoFincaAnimalRequest> Animales { get; set; } = [];
}

public class RegistrarTrasladoFincaRequest : ValidarTrasladoFincaRequest
{
    public DateTime Fecha_Traslado { get; set; }
    public string? Observacion { get; set; }
}

public class RegistrarTrasladoFincaLoteRequest
{
    public long Finca_Origen_Codigo { get; set; }
    public long Finca_Destino_Codigo { get; set; }
    public long Potrero_Destino_Codigo { get; set; }
    public DateTime Fecha_Traslado { get; set; }
    public string? Observacion { get; set; }
    public List<TrasladoFincaAnimalRequest> Animales { get; set; } = [];
}

public class TrasladoFincaAnimalRequest
{
    public long Animal_Codigo { get; set; }
}
