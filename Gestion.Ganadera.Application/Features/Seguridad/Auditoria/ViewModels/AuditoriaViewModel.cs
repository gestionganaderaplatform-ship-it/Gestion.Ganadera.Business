using Gestion.Ganadera.Application.Abstractions.Interfaces;
using AuditoriaEntity = Gestion.Ganadera.Domain.Features.Seguridad.Auditoria;

namespace Gestion.Ganadera.Application.Features.Seguridad.Auditoria.ViewModels
{
    public class AuditoriaViewModel : IMapsToEntity<AuditoriaEntity>
    {
        public long Auditoria_Codigo { get; set; }
        public string? Auditoria_Api_Codigo { get; set; }
        public string? Auditoria_Nombre_Tabla { get; set; }
        public string? Auditoria_Valor_Clave { get; set; }
        public string? Auditoria_Valores_Viejos { get; set; }
        public string? Auditoria_Nuevos_Valores { get; set; }
        public string? Auditoria_Modificado_Por { get; set; }
        public DateTime Auditoria_Fecha_Modificado { get; set; }
    }

    public class AuditoriaCreateViewModel
    {
    }

    public class AuditoriaUpdateViewModel
    {
    }

    public class AuditoriaExportFilterViewModel
    {
        public string? Auditoria_Nombre_Tabla { get; set; }
        public string? Auditoria_Modificado_Por { get; set; }
        public DateTime? Auditoria_Fecha_Modificado_Desde { get; set; }
        public DateTime? Auditoria_Fecha_Modificado_Hasta { get; set; }
    }
}
