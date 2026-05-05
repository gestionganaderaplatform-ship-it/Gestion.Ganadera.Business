namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Models;

public class ValidarMovimientoPotreroRequest
{
    public long Finca_Codigo { get; set; }
    public long Potrero_Destino_Codigo { get; set; }
    public List<long> Animales_Codigos { get; set; } = [];
}

public class ValidarMovimientoPotreroLoteRequest
{
    public long Finca_Codigo { get; set; }
    public long Potrero_Destino_Codigo { get; set; }
    public DateTime Fecha_Movimiento { get; set; }
    public List<MovimientoPotreroAnimalRequest> Animales { get; set; } = [];
}

public class RegistrarMovimientoPotreroRequest : ValidarMovimientoPotreroRequest
{
    public DateTime Fecha_Movimiento { get; set; }
    public string? Observacion { get; set; }
}

public class RegistrarMovimientoPotreroLoteRequest
{
    public long Finca_Codigo { get; set; }
    public long Potrero_Destino_Codigo { get; set; }
    public DateTime Fecha_Movimiento { get; set; }
    public string? Observacion { get; set; }
    public List<MovimientoPotreroAnimalRequest> Animales { get; set; } = [];
}

public class MovimientoPotreroAnimalRequest
{
    public long Animal_Codigo { get; set; }
}
