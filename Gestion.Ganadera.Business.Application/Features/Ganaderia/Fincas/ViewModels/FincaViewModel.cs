using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using FincaEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.Finca;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.ViewModels;

public class FincaViewModel : IMapsToEntity<FincaEntity>
{
    public long Finca_Codigo { get; set; }
    public string Finca_Nombre { get; set; } = string.Empty;
    public bool Finca_Activa { get; set; } = true;
}

public class FincaCreateViewModel : IMapsToEntity<FincaEntity>
{
    public string Finca_Nombre { get; set; } = string.Empty;
    public bool Finca_Activa { get; set; } = true;
}

public class FincaUpdateViewModel : IMapsToEntity<FincaEntity>
{
    public long Finca_Codigo { get; set; }
    public string Finca_Nombre { get; set; } = string.Empty;
    public bool Finca_Activa { get; set; } = true;
}
