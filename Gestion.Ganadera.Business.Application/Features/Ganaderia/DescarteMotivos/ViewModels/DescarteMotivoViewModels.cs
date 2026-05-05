using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.ViewModels;

public class DescarteMotivoViewModel : IMapsToEntity<DescarteMotivo>
{
    public long Descarte_Motivo_Codigo { get; set; }
    public string Descarte_Motivo_Nombre { get; set; } = string.Empty;
    public bool Descarte_Motivo_Activo { get; set; }
}

public class CreateDescarteMotivoViewModel : IMapsToEntity<DescarteMotivo>
{
    public string Descarte_Motivo_Nombre { get; set; } = string.Empty;
}

public class UpdateDescarteMotivoViewModel : IMapsToEntity<DescarteMotivo>
{
    public long Descarte_Motivo_Codigo { get; set; }
    public string Descarte_Motivo_Nombre { get; set; } = string.Empty;
    public bool Descarte_Motivo_Activo { get; set; }
}
