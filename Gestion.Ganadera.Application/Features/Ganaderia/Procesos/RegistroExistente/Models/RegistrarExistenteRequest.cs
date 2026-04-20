namespace Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;

public class RegistrarExistenteRequest : ValidarRegistroExistenteRequest
{
    public DateTime Fecha_Informada { get; set; }
    public string? Observacion { get; set; }
}
