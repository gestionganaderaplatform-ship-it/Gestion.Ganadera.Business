using Gestion.Ganadera.Business.Domain.Base;

namespace Gestion.Ganadera.Business.Domain.Features.Ganaderia;

public class Animal : AuditableEntity
{
    public long Animal_Codigo { get; set; }
    public long Finca_Codigo { get; set; }
    public long Potrero_Codigo { get; set; }
    public long Categoria_Animal_Codigo { get; set; }
    public string Animal_Sexo { get; set; } = string.Empty;
    public bool Animal_Activo { get; set; } = true;
    public string Animal_Origen_Ingreso { get; set; } = string.Empty;
    public DateTime? Animal_Fecha_Nacimiento { get; set; }
    public DateTime Animal_Fecha_Ingreso_Inicial { get; set; }
    public DateTime Animal_Fecha_Registro_Ingreso { get; set; }
    public DateTime Animal_Fecha_Ultimo_Evento { get; set; }
    public decimal? Animal_Peso { get; set; }
    public DateTime? Animal_Fecha_Peso { get; set; }
    public DateTime? Animal_Fecha_Muerte { get; set; }
    public long? Causa_Muerte_Codigo { get; set; }
    public DateTime? Animal_Fecha_Venta { get; set; }
    public DateTime? Animal_Fecha_Descarte { get; set; }
    public long? Descarte_Motivo_Codigo { get; set; }

    // Estados derivados de salud
    public DateTime? Animal_Ultimo_Evento_Sanitario_Fecha { get; set; }
    public string? Animal_Ultimo_Evento_Sanitario_Tipo { get; set; }
    public string? Animal_Ultimo_Evento_Sanitario_Producto { get; set; }

    // Estados derivados reproductivos
    public DateTime? Animal_Ultima_Palpacion_Fecha { get; set; }
    public string? Animal_Ultimo_Resultado_Reproductivo { get; set; }
    public string? Animal_Estado_Reproductivo_Actual { get; set; }

    // Propiedades de navegacion
    public virtual Finca? Finca { get; set; }
    public virtual Potrero? Potrero { get; set; }
    public virtual CategoriaAnimal? Categoria { get; set; }
}
