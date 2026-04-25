namespace Gestion.Ganadera.Business.Application.Features.Base.Interfaces
{
    /// <summary>
    /// Contrato base para persistencia CRUD y consultas genericas de una entidad.
    /// </summary>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> Consultar(long codigo);
        Task<List<TEntity>> ObtenerTodos();
        Task<bool> Insertar(TEntity entidad);
        Task<bool> InsertarMasivamente(IEnumerable<TEntity> entidades);
        Task<bool> Actualizar(TEntity entidad);
        Task<bool> ActualizarParcial(TEntity entidad, ISet<string> propiedades);
        Task<bool> Eliminar(long codigo);
        Task<bool> EliminarPorCodigoForaneo<TProperty>(string propiedadForanea, TProperty valorForaneo);
        Task<List<TEntity>> ConsultarPorForanea<TProperty>(string propiedadForanea, TProperty valorForaneo);
        Task<bool> Existe(long codigo);
        Task<(List<TProperty> Existentes, List<TProperty> NoExistentes)> ExistenVarios<TProperty>(IEnumerable<TProperty> codigos, string propiedadClave);
        Task<(List<TEntity> Items, int TotalRegistros)> ObtenerPorPaginado(int pagina, int tamañoPagina);
        Task<bool> ExistePorForanea<TProperty>(string propiedadForanea, TProperty valorForaneo);
        Task<List<TEntity>> FiltrarPorPropiedades(Dictionary<string, object> filtros);
        Task<(List<TEntity> Items, int TotalRegistros)> FiltrarPorPropiedadesPaginado(
            Dictionary<string, object> filtros,
            int pageNumber,
            int pageSize);
    }
}

