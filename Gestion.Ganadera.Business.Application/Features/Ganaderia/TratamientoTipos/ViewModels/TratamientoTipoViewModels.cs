using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.ViewModels;

public class TratamientoTipoViewModel : IMapsToEntity<TratamientoTipo>
{
    public long Tratamiento_Tipo_Codigo { get; set; }
    public string Tratamiento_Tipo_Nombre { get; set; } = string.Empty;
    public bool Tratamiento_Tipo_Activa { get; set; }
}

public class TratamientoTipoCreateViewModel : IMapsToEntity<TratamientoTipo>
{
    public string Tratamiento_Tipo_Nombre { get; set; } = string.Empty;
}

public class TratamientoTipoUpdateViewModel : IMapsToEntity<TratamientoTipo>
{
    public long Tratamiento_Tipo_Codigo { get; set; }
    public string Tratamiento_Tipo_Nombre { get; set; } = string.Empty;
    public bool Tratamiento_Tipo_Activa { get; set; }
}
