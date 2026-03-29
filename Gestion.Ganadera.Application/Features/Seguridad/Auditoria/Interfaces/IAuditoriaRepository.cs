using Gestion.Ganadera.Application.Features.Base.Interfaces;
using AuditoriaEntity = Gestion.Ganadera.Domain.Features.Seguridad.Auditoria;

namespace Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Interfaces
{
    public interface IAuditoriaRepository : IBaseRepository<AuditoriaEntity>
    {
    }
}
