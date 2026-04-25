using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using TipoIdentificadorEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.TipoIdentificador;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.ViewModels;

public class TipoIdentificadorViewModel : IMapsToEntity<TipoIdentificadorEntity>
{
    public long Tipo_Identificador_Codigo { get; set; }
    public string Tipo_Identificador_Nombre { get; set; } = string.Empty;
    public string? Tipo_Identificador_Codigo_Interno { get; set; }
    public bool Tipo_Identificador_Operativo { get; set; } = true;
    public bool Tipo_Identificador_Permite_Busqueda { get; set; } = true;
    public bool Tipo_Identificador_Permite_Principal { get; set; } = true;
    public bool Tipo_Identificador_Activo { get; set; } = true;
}

public class TipoIdentificadorCreateViewModel : IMapsToEntity<TipoIdentificadorEntity>
{
    public string Tipo_Identificador_Nombre { get; set; } = string.Empty;
    public string? Tipo_Identificador_Codigo_Interno { get; set; }
    public bool Tipo_Identificador_Operativo { get; set; } = true;
    public bool Tipo_Identificador_Permite_Busqueda { get; set; } = true;
    public bool Tipo_Identificador_Permite_Principal { get; set; } = true;
    public bool Tipo_Identificador_Activo { get; set; } = true;
}

public class TipoIdentificadorUpdateViewModel : IMapsToEntity<TipoIdentificadorEntity>
{
    public long Tipo_Identificador_Codigo { get; set; }
    public string Tipo_Identificador_Nombre { get; set; } = string.Empty;
    public string? Tipo_Identificador_Codigo_Interno { get; set; }
    public bool Tipo_Identificador_Operativo { get; set; } = true;
    public bool Tipo_Identificador_Permite_Busqueda { get; set; } = true;
    public bool Tipo_Identificador_Permite_Principal { get; set; } = true;
    public bool Tipo_Identificador_Activo { get; set; } = true;
}
