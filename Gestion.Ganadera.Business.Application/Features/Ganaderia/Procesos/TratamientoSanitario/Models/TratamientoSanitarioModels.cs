namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Models;

public class RegistrarTratamientoRequest
{
    public long Animal_Codigo { get; set; }
    public long Tratamiento_Producto_Codigo { get; set; }
    public DateTime Fecha_Aplicacion { get; set; }
    public decimal? Dosis { get; set; }
    public string? Duracion { get; set; }
    public string? Indicacion { get; set; }
    public string? Aplicador { get; set; }
    public string? Observacion { get; set; }
}

public class ValidarTratamientoRequest
{
    public long Animal_Codigo { get; set; }
    public long Tratamiento_Producto_Codigo { get; set; }
    public DateTime Fecha_Aplicacion { get; set; }
}

public class RegistrarTratamientoLoteRequest
{
    public List<long> Animales { get; set; } = [];
    public long Tratamiento_Producto_Codigo { get; set; }
    public DateTime Fecha_Aplicacion { get; set; }
    public decimal? Dosis { get; set; }
    public string? Duracion { get; set; }
    public string? Indicacion { get; set; }
    public string? Aplicador { get; set; }
    public string? Observacion { get; set; }
}

public class ValidarTratamientoLoteRequest
{
    public List<long> Animales { get; set; } = [];
    public long Tratamiento_Producto_Codigo { get; set; }
    public DateTime Fecha_Aplicacion { get; set; }
}
