using System.Linq.Expressions;
using System.Reflection;
using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Base
{
    /// <summary>
    /// Repositorio base para operaciones CRUD y consultas dinamicas sobre entidades persistidas con EF Core.
    /// </summary>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> Consultar(long codigo)
        {
            return await _dbSet.FindAsync(codigo);
        }

        public async Task<List<TEntity>> ObtenerTodos()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> Insertar(TEntity entidad)
        {
            await _dbSet.AddAsync(entidad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertarMasivamente(IEnumerable<TEntity> entidades)
        {
            var lista = entidades.ToList();

            await _context.Set<TEntity>().AddRangeAsync(lista);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Actualizar(TEntity entidad)
        {
            var entry = _context.Entry(entidad);
            var key = entry.Metadata.FindPrimaryKey()?.Properties[0];

            if (key == null)
                return false;

            var keyValue = entry.Property(key.Name).CurrentValue;

            var existingEntity = await _dbSet.FindAsync(keyValue);
            if (existingEntity == null)
                return false;

            var existingEntry = _context.Entry(existingEntity);
            existingEntry.CurrentValues.SetValues(entidad);

            foreach (var property in existingEntry.Properties)
            {
                if (property.Metadata.IsPrimaryKey())
                    continue;

                var value = property.CurrentValue;
                property.IsModified = value switch
                {
                    null => false,
                    string str => !string.IsNullOrWhiteSpace(str),
                    DateTime dt => dt != DateTime.MinValue,
                    _ => true
                };
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ActualizarParcial(TEntity entidad, ISet<string> propiedades)
        {
            if (propiedades is null || propiedades.Count == 0)
                return false;

            var sourceEntry = _context.Entry(entidad);
            var key = sourceEntry.Metadata.FindPrimaryKey()?.Properties[0];

            if (key == null)
                return false;

            var keyValue = sourceEntry.Property(key.Name).CurrentValue;
            var existingEntity = await _dbSet.FindAsync(keyValue);

            if (existingEntity == null)
                return false;

            var existingEntry = _context.Entry(existingEntity);
            var propiedadesActualizadas = 0;

            foreach (var propiedad in propiedades)
            {
                var propiedadMetadata = existingEntry.Metadata.FindProperty(propiedad);
                if (propiedadMetadata == null || propiedadMetadata.IsPrimaryKey())
                    continue;

                var valor = sourceEntry.Property(propiedad).CurrentValue;
                existingEntry.Property(propiedad).CurrentValue = valor;
                existingEntry.Property(propiedad).IsModified = true;
                propiedadesActualizadas++;
            }

            if (propiedadesActualizadas == 0)
                return false;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(long codigo)
        {
            var entidad = await _dbSet.FindAsync(codigo);
            if (entidad == null)
                return false;

            _dbSet.Remove(entidad);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarPorCodigoForaneo<TProperty>(string propiedadForanea, TProperty valorForaneo)
        {
            if (string.IsNullOrWhiteSpace(propiedadForanea) || valorForaneo is null)
                return false;

            var entidades = await _dbSet
                .Where(e => EF.Property<TProperty>(e, propiedadForanea) != null &&
                            EF.Property<TProperty>(e, propiedadForanea)!.Equals(valorForaneo))
                .ToListAsync();

            if (entidades.Count == 0)
                return false;

            _dbSet.RemoveRange(entidades);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<TEntity>> ConsultarPorForanea<TProperty>(string propiedadForanea, TProperty valorForaneo)
        {
            var propertyInfo = typeof(TEntity).GetProperty(
                propiedadForanea,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var property = Expression.Property(parameter, propertyInfo!);
            var constant = Expression.Constant(valorForaneo, typeof(TProperty));
            var equals = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            return await _dbSet.Where(lambda).ToListAsync();
        }

        public async Task<bool> Existe(long codigo)
        {
            return await _context.Set<TEntity>().FindAsync(codigo) != null;
        }

        public async Task<(List<TProperty> Existentes, List<TProperty> NoExistentes)> ExistenVarios<TProperty>(
            IEnumerable<TProperty> codigos,
            string propiedadClave)
        {
            var codigosUnicos = codigos
                .Distinct()
                .ToList();

            var existentes = await _context.Set<TEntity>()
                .Where(e => codigosUnicos.Contains(EF.Property<TProperty>(e, propiedadClave)))
                .Select(e => EF.Property<TProperty>(e, propiedadClave))
                .Distinct()
                .ToListAsync();

            var noExistentes = codigosUnicos.Except(existentes).ToList();

            return (existentes, noExistentes);
        }

        public async Task<(List<TEntity> Items, int TotalRegistros)> ObtenerPorPaginado(int pagina, int tamanoPagina)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            int totalRegistros = await query.CountAsync();
            List<TEntity> items = await AplicarOrdenPredeterminado(query)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToListAsync();

            return (items, totalRegistros);
        }

        public async Task<bool> ExistePorForanea<TProperty>(string propiedadForanea, TProperty valorForaneo)
        {
            var parametro = Expression.Parameter(typeof(TEntity), "x");
            var propiedad = Expression.Property(parametro, propiedadForanea);
            var constante = Expression.Constant(valorForaneo, typeof(TProperty));
            var igualdad = Expression.Equal(propiedad, constante);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(igualdad, parametro);

            return await _context.Set<TEntity>().AnyAsync(lambda);
        }

        public async Task<List<TEntity>> FiltrarPorPropiedades(Dictionary<string, object> filtros)
        {
            var query = ConstruirConsultaFiltrada(filtros);
            return await query.ToListAsync();
        }

        public async Task<(List<TEntity> Items, int TotalRegistros)> FiltrarPorPropiedadesPaginado(
            Dictionary<string, object> filtros,
            int pageNumber,
            int pageSize)
        {
            var query = ConstruirConsultaFiltrada(filtros);
            var totalRegistros = await query.CountAsync();
            var items = await AplicarOrdenPredeterminado(query)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalRegistros);
        }

        protected IQueryable<TEntity> ConstruirConsultaFiltrada(Dictionary<string, object> filtros)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            var tipoEntidad = typeof(TEntity);
            var parametro = Expression.Parameter(tipoEntidad, "x");

            foreach (var filtro in filtros)
            {
                var propiedadInfo = tipoEntidad.GetProperty(filtro.Key);
                if (propiedadInfo == null)
                    throw new InvalidOperationException(
                        $"La propiedad '{filtro.Key}' no existe en '{tipoEntidad.Name}'.");

                var propiedad = Expression.Property(parametro, propiedadInfo);
                var tipoPropiedad = Nullable.GetUnderlyingType(propiedadInfo.PropertyType) ?? propiedadInfo.PropertyType;
                var valorConvertido = Convert.ChangeType(filtro.Value, tipoPropiedad);
                var constante = Expression.Constant(valorConvertido, tipoPropiedad);

                Expression condicion;
                if (tipoPropiedad == typeof(DateTime) || tipoPropiedad == typeof(DateTime?))
                {
                    condicion = Expression.GreaterThanOrEqual(propiedad, constante);
                }
                else if (tipoPropiedad == typeof(string))
                {
                    condicion = Expression.Call(propiedad, "Contains", null, constante);
                }
                else if (tipoPropiedad == typeof(bool) || tipoPropiedad == typeof(bool?))
                {
                    condicion = Expression.Equal(propiedad, constante);
                }
                else if (tipoPropiedad.IsNumericType())
                {
                    condicion = Expression.GreaterThanOrEqual(propiedad, constante);
                }
                else
                {
                    condicion = Expression.Equal(propiedad, constante);
                }

                var lambda = Expression.Lambda<Func<TEntity, bool>>(condicion, parametro);
                query = query.Where(lambda);
            }

            return query;
        }

        private IQueryable<TEntity> AplicarOrdenPredeterminado(IQueryable<TEntity> query)
        {
            var entityType = _context.Model.FindEntityType(typeof(TEntity));
            var primaryKey = entityType?.FindPrimaryKey()?.Properties.FirstOrDefault();

            if (primaryKey is null)
                return query;

            var parametro = Expression.Parameter(typeof(TEntity), "x");
            var propiedad = Expression.Call(
                typeof(EF),
                nameof(EF.Property),
                [primaryKey.ClrType],
                parametro,
                Expression.Constant(primaryKey.Name));

            var lambda = Expression.Lambda(propiedad, parametro);

            var orderByMethod = typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Single(method =>
                    method.Name == nameof(Queryable.OrderBy)
                    && method.GetParameters().Length == 2);

            var genericOrderByMethod = orderByMethod.MakeGenericMethod(typeof(TEntity), primaryKey.ClrType);

            return (IQueryable<TEntity>)genericOrderByMethod.Invoke(null, [query, lambda])!;
        }
    }
}
