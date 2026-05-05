using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.ViewModels;

public class PalpacionResultadoViewModel : IMapsToEntity<PalpacionResultado>
{
    public long Palpacion_Resultado_Codigo { get; set; }
    public string Palpacion_Resultado_Nombre { get; set; } = string.Empty;
    public bool Palpacion_Resultado_Activo { get; set; }
}

public class PalpacionResultadoCreateViewModel : IMapsToEntity<PalpacionResultado>
{
    public string Palpacion_Resultado_Nombre { get; set; } = string.Empty;
}

public class PalpacionResultadoUpdateViewModel : IMapsToEntity<PalpacionResultado>
{
    public long Palpacion_Resultado_Codigo { get; set; }
    public string Palpacion_Resultado_Nombre { get; set; } = string.Empty;
    public bool Palpacion_Resultado_Activo { get; set; }
}
