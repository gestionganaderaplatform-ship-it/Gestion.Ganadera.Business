using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.ViewModels;

public class CausaMuerteViewModel : IMapsToEntity<CausaMuerte>
{
    public long Causa_Muerte_Codigo { get; set; }
    public string Causa_Muerte_Nombre { get; set; } = string.Empty;
    public string? Causa_Muerte_Descripcion { get; set; }
    public bool Causa_Muerte_Activa { get; set; }
    public int? Causa_Muerte_Orden { get; set; }
}

public class CausaMuerteCreateViewModel : IMapsToEntity<CausaMuerte>
{
    public string Causa_Muerte_Nombre { get; set; } = string.Empty;
    public string? Causa_Muerte_Descripcion { get; set; }
    public bool Causa_Muerte_Activa { get; set; } = true;
    public int? Causa_Muerte_Orden { get; set; }
}

public class CausaMuerteUpdateViewModel : IMapsToEntity<CausaMuerte>
{
    public long Causa_Muerte_Codigo { get; set; }
    public string Causa_Muerte_Nombre { get; set; } = string.Empty;
    public string? Causa_Muerte_Descripcion { get; set; }
    public bool Causa_Muerte_Activa { get; set; }
    public int? Causa_Muerte_Orden { get; set; }
}
