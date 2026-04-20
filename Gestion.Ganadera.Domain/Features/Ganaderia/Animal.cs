using Gestion.Ganadera.Domain.Base;

namespace Gestion.Ganadera.Domain.Features.Ganaderia;

public class Animal : AuditableEntity
{
    public long Animal_Codigo { get; set; }
    public long Finca_Codigo { get; set; }
    public long Potrero_Codigo { get; set; }
    public long Categoria_Animal_Codigo { get; set; }
    public string Animal_Sexo { get; set; } = string.Empty;
    public bool Animal_Activo { get; set; } = true;
    public string Animal_Origen_Ingreso { get; set; } = string.Empty;
    public DateTime Animal_Fecha_Ingreso_Inicial { get; set; }
    public DateTime Animal_Fecha_Registro_Ingreso { get; set; }
    public DateTime Animal_Fecha_Ultimo_Evento { get; set; }
}
