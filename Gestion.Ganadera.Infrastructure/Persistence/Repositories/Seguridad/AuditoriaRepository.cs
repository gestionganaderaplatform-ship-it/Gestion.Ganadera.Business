using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Interfaces;
using Gestion.Ganadera.Domain.Features.Seguridad;
using Gestion.Ganadera.Infrastructure.Persistence.Repositories.Base;

namespace Gestion.Ganadera.Infrastructure.Persistence.Repositories.Seguridad
{
    public class AuditoriaRepository(AppDbContext context) : BaseRepository<Auditoria>(context), IAuditoriaRepository
    {
    }
}
