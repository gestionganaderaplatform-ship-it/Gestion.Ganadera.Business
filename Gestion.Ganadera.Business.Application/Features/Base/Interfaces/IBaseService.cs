using Gestion.Ganadera.Business.Application.Abstractions.Model;

namespace Gestion.Ganadera.Business.Application.Features.Base.Interfaces
{
    /// <summary>
    /// Contrato base para casos de uso CRUD expuestos por una feature del template.
    /// </summary>
    public interface IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel>
    {
        Task<TViewModel?> Consultar(long codigo);
        Task<IEnumerable<TViewModel>> ObtenerTodos();
        Task<TViewModel?> Insertar(TCreateViewModel entidad);
        Task<bool> InsertarMasivamente(IEnumerable<TCreateViewModel> entidades);
        Task<bool> Actualizar(TUpdateViewModel entidad);
        Task<bool> ActualizarParcial(TUpdateViewModel entidad, ISet<string> propiedades);
        Task<bool> Eliminar(long codigo);
        Task<bool> EliminarPorCodigoForaneo<TProperty>(string propiedadForanea, TProperty valorForaneo);
        Task<IEnumerable<TViewModel>> ConsultarPorForanea<TProperty>(string propiedadForanea, TProperty valorForaneo);
        Task<bool> Existe(long codigo);
        Task<(List<TProperty> Existentes, List<TProperty> NoExistentes)> ExistenVarios<TProperty>(IEnumerable<TProperty> codigos, string propiedadClave);
        Task<(IEnumerable<TViewModel> Items, int TotalRegistros)> ObtenerPorPaginado(int pagina, int pageSize);
        Task<bool> ExistePorForanea<TProperty>(string propiedadForanea, TProperty valorForaneo);
        Task<IEnumerable<TViewModel>> FiltrarPorPropiedadesAsync(Dictionary<string, object> filtros);
        Task<(IEnumerable<TViewModel> Items, int TotalRegistros)> FiltrarPorPropiedadesPaginadoAsync(
            Dictionary<string, object> filtros,
            int pageNumber,
            int pageSize);
    }

    /// <summary>
    /// Variante del servicio base que agrega exportacion Excel con un filtro propio de la feature.
    /// </summary>
    public interface IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter>
        : IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel>
    {
        Task<ExcelFileResult> ExportarExcelAsync(TExportFilter filtro);
    }

    /// <summary>
    /// Variante del servicio base que agrega importacion y plantilla Excel sobre la misma familia base.
    /// </summary>
    public interface IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter, TImportModel>
        : IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter>
    {
        Task<ImportResult> ImportarExcelAsync(Stream archivo);
        Task<ExcelFileResult> DescargarPlantillaImportacionAsync();
    }
}
