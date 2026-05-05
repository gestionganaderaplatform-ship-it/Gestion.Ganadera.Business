using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class Vacuna : AuditableEntity
{
    public long Vacuna_Codigo { get; set; }
    public string Vacuna_Nombre { get; set; } = string.Empty;
    public long? Vacuna_Enfermedad_Codigo { get; set; }
    public VacunaEnfermedad? Vacuna_Enfermedad { get; set; }
    public bool Vacuna_Activa { get; set; } = true;
}

public class VacunaEnfermedad : AuditableEntity
{
    public long Vacuna_Enfermedad_Codigo { get; set; }
    public string Vacuna_Enfermedad_Nombre { get; set; } = string.Empty;
    public bool Vacuna_Enfermedad_Activa { get; set; } = true;
}