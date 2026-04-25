using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using AuditoriaEntity = Gestion.Ganadera.Business.Domain.Features.Seguridad.Auditoria;

namespace Gestion.Ganadera.Business.Application.Features.Seguridad.Auditoria.Interfaces
{
    public interface IAuditoriaRepository : IBaseRepository<AuditoriaEntity>
    {
    }
}
