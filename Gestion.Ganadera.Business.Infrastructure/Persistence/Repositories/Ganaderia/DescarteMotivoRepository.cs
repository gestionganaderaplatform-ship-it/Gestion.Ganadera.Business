using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia;

public class DescarteMotivoRepository(AppDbContext context) 
    : BaseRepository<DescarteMotivo>(context), IDescarteMotivoRepository
{
    public async Task<bool> ExisteNombreAsync(string nombre, long? excluirId = null, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(x => 
            x.Descarte_Motivo_Nombre.ToLower() == nombre.ToLower() && 
            (!excluirId.HasValue || x.Descarte_Motivo_Codigo != excluirId.Value), 
            cancellationToken);
    }
}
