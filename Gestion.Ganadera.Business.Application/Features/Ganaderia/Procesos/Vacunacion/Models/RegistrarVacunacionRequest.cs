namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Models;

public class ValidarVacunacionRequest
{
    public long Finca_Codigo { get; set; }
    public List<long> Animales_Codigos { get; set; } = [];
    public long Vacuna_Codigo { get; set; }
    public long? Vacuna_Enfermedad_Codigo { get; set; }
    public string? Ciclo_Vacunacion { get; set; }
    public string? Lote_Biologico { get; set; }
}

public class ValidarVacunacionLoteRequest
{
    public long Finca_Codigo { get; set; }
    public long Vacuna_Codigo { get; set; }
    public long? Vacuna_Enfermedad_Codigo { get; set; }
    public string? Ciclo_Vacunacion { get; set; }
    public string? Lote_Biologico { get; set; }
    public DateTime Fecha_Aplicacion { get; set; }
    public List<VacunacionAnimalRequest> Animales { get; set; } = [];
}

public class RegistrarVacunacionRequest : ValidarVacunacionRequest
{
    public DateTime Fecha_Aplicacion { get; set; }
    public string? Vacunador { get; set; }
    public string? Dosis { get; set; }
    public string? Soporte_Certificado_Nombre { get; set; }
    public string? Observacion { get; set; }
}

public class RegistrarVacunacionLoteRequest
{
    public long Finca_Codigo { get; set; }
    public long Vacuna_Codigo { get; set; }
    public long? Vacuna_Enfermedad_Codigo { get; set; }
    public string? Ciclo_Vacunacion { get; set; }
    public string? Lote_Biologico { get; set; }
    public DateTime Fecha_Aplicacion { get; set; }
    public string? Vacunador { get; set; }
    public string? Dosis { get; set; }
    public string? Soporte_Certificado_Nombre { get; set; }
    public string? Observacion { get; set; }
    public List<VacunacionAnimalRequest> Animales { get; set; } = [];
}

public class VacunacionAnimalRequest
{
    public long Animal_Codigo { get; set; }
}