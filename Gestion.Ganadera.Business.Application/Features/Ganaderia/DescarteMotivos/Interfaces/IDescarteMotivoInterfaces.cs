using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.Interfaces;

public interface IDescarteMotivoRepository : IBaseRepository<DescarteMotivo>
{
    Task<bool> ExisteNombreAsync(string nombre, long? excluirId = null, CancellationToken cancellationToken = default);
}

public interface IDescarteMotivoService : IBaseService<DescarteMotivoViewModel, CreateDescarteMotivoViewModel, UpdateDescarteMotivoViewModel>
{
}
