namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Models;

public class RegistrarPesajeRequest
{
    public long Finca_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public DateTime Fecha_Pesaje { get; set; }
    public decimal Peso { get; set; }
    public string? Observacion { get; set; }
}

public class ValidarPesajeRequest
{
    public long Finca_Codigo { get; set; }
    public long Animal_Codigo { get; set; }
    public DateTime Fecha_Pesaje { get; set; }
    public decimal Peso { get; set; }
}

public class RegistrarPesajeLoteRequest
{
    public long Finca_Codigo { get; set; }
    public DateTime Fecha_Pesaje { get; set; }
    public string? Observacion { get; set; }
    public List<PesajeAnimalRequest> Animales { get; set; } = new();
}

public class ValidarPesajeLoteRequest
{
    public long Finca_Codigo { get; set; }
    public DateTime Fecha_Pesaje { get; set; }
    public List<PesajeAnimalRequest> Animales { get; set; } = new();
}

public class PesajeAnimalRequest
{
    public long Animal_Codigo { get; set; }
    public decimal Peso { get; set; }
}