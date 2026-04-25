using Gestion.Ganadera.Business.Application.Features.Seguridad.Auditoria.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Seguridad;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Base;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Seguridad
{
    public class AuditoriaRepository(AppDbContext context) : BaseRepository<Auditoria>(context), IAuditoriaRepository
    {
    }
}
