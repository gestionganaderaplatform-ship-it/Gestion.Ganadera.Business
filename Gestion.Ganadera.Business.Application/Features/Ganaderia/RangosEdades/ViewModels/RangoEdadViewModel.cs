using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using RangoEdadEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.RangoEdad;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.RangosEdades.ViewModels;

public class RangoEdadViewModel : IMapsToEntity<RangoEdadEntity>
{
    public long Rango_Edad_Codigo { get; set; }
    public string Rango_Edad_Nombre { get; set; } = string.Empty;
    public int? Rango_Edad_Edad_Minima_Meses { get; set; }
    public int? Rango_Edad_Edad_Maxima_Meses { get; set; }
    public int Rango_Edad_Orden { get; set; }
    public bool Rango_Edad_Activo { get; set; } = true;
}

public class RangoEdadCreateViewModel : IMapsToEntity<RangoEdadEntity>
{
    public string Rango_Edad_Nombre { get; set; } = string.Empty;
    public int? Rango_Edad_Edad_Minima_Meses { get; set; }
    public int? Rango_Edad_Edad_Maxima_Meses { get; set; }
    public int Rango_Edad_Orden { get; set; }
    public bool Rango_Edad_Activo { get; set; } = true;
}

public class RangoEdadUpdateViewModel : IMapsToEntity<RangoEdadEntity>
{
    public long Rango_Edad_Codigo { get; set; }
    public string Rango_Edad_Nombre { get; set; } = string.Empty;
    public int? Rango_Edad_Edad_Minima_Meses { get; set; }
    public int? Rango_Edad_Edad_Maxima_Meses { get; set; }
    public int Rango_Edad_Orden { get; set; }
    public bool Rango_Edad_Activo { get; set; } = true;
}
