namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Models;

public class RegistrarNacimientoRequest : ValidarNacimientoRequest
{
    public decimal? Peso_Nacer { get; set; }
    public string? Observacion { get; set; }
}
