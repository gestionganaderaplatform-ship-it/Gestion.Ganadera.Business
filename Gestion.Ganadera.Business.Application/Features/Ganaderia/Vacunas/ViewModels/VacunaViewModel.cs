using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using VacunaEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.Vacuna;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.ViewModels;

public class VacunaViewModel : IMapsToEntity<VacunaEntity>
{
    public long Vacuna_Codigo { get; set; }
    public string Vacuna_Nombre { get; set; } = string.Empty;
    public long? Vacuna_Enfermedad_Codigo { get; set; }
    public string? Vacuna_Enfermedad_Nombre { get; set; }
    public bool Vacuna_Activa { get; set; } = true;
}

public class VacunaCreateViewModel : IMapsToEntity<VacunaEntity>
{
    public string Vacuna_Nombre { get; set; } = string.Empty;
    public long? Vacuna_Enfermedad_Codigo { get; set; }
    public bool Vacuna_Activa { get; set; } = true;
}

public class VacunaUpdateViewModel : IMapsToEntity<VacunaEntity>
{
    public long Vacuna_Codigo { get; set; }
    public string Vacuna_Nombre { get; set; } = string.Empty;
    public long? Vacuna_Enfermedad_Codigo { get; set; }
    public bool Vacuna_Activa { get; set; } = true;
}