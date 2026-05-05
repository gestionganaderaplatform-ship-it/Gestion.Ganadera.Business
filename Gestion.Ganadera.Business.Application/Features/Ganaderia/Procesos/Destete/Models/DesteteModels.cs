namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Models;

public class RegistrarDesteteRequest
{
    public long Animal_Codigo_Cria { get; set; }
    public long Animal_Codigo_Madre { get; set; }
    public long? Potrero_Destino_Codigo { get; set; }
    public DateTime Fecha_Destete { get; set; }
    public string? Responsable { get; set; }
    public string? Observacion { get; set; }
}

public class ValidarDesteteRequest
{
    public long Animal_Codigo_Cria { get; set; }
    public long Animal_Codigo_Madre { get; set; }
    public long? Potrero_Destino_Codigo { get; set; }
    public DateTime Fecha_Destete { get; set; }
}

public class RegistrarDesteteLoteItem
{
    public long Animal_Codigo_Cria { get; set; }
    public long Animal_Codigo_Madre { get; set; }
}

public class RegistrarDesteteLoteRequest
{
    public List<RegistrarDesteteLoteItem> Items { get; set; } = [];
    public long? Potrero_Destino_Codigo { get; set; }
    public DateTime Fecha_Destete { get; set; }
    public string? Responsable { get; set; }
    public string? Observacion { get; set; }
}

public class ValidarDesteteLoteItem
{
    public long Animal_Codigo_Cria { get; set; }
    public long Animal_Codigo_Madre { get; set; }
}

public class ValidarDesteteLoteRequest
{
    public List<ValidarDesteteLoteItem> Items { get; set; } = [];
    public long? Potrero_Destino_Codigo { get; set; }
    public DateTime Fecha_Destete { get; set; }
}
