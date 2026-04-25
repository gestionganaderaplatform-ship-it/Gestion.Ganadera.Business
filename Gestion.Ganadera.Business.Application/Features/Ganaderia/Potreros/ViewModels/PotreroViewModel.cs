using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using PotreroEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.Potrero;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.ViewModels;

public class PotreroViewModel : IMapsToEntity<PotreroEntity>
{
    public long Potrero_Codigo { get; set; }
    public long Finca_Codigo { get; set; }
    public string Potrero_Nombre { get; set; } = string.Empty;
    public bool Potrero_Activo { get; set; } = true;
}

public class PotreroCreateViewModel : IMapsToEntity<PotreroEntity>
{
    public long Finca_Codigo { get; set; }
    public string Potrero_Nombre { get; set; } = string.Empty;
    public bool Potrero_Activo { get; set; } = true;
}

public class PotreroUpdateViewModel : IMapsToEntity<PotreroEntity>
{
    public long Potrero_Codigo { get; set; }
    public long Finca_Codigo { get; set; }
    public string Potrero_Nombre { get; set; } = string.Empty;
    public bool Potrero_Activo { get; set; } = true;
}
