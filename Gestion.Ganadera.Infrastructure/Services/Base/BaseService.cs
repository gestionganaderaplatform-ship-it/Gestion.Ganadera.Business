using AutoMapper;
using ClosedXML.Excel;
using FluentValidation;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Abstractions.Model;
using Gestion.Ganadera.Application.Common.Extensions;
using Gestion.Ganadera.Application.Features.Base.Interfaces;

namespace Gestion.Ganadera.Infrastructure.Services.Base
{
    /// <summary>
    /// Servicio base que conecta view models con repositorios genericos mediante AutoMapper.
    /// </summary>
    public class BaseService<TEntity, TView, TCreateView, TUpdateView, TRepository>(TRepository repository, IMapper mapper)
        : IBaseService<TView, TCreateView, TUpdateView>
        where TEntity : class
        where TRepository : IBaseRepository<TEntity>
    {
        protected readonly TRepository _repository = repository;
        protected readonly IMapper _mapper = mapper;

        public virtual async Task<TView?> Consultar(long codigo)
        {
            var entity = await _repository.Consultar(codigo);
            return _mapper.Map<TView>(entity);
        }

        public virtual async Task<IEnumerable<TView>> ObtenerTodos()
        {
            var entities = await _repository.ObtenerTodos();
            return _mapper.Map<IEnumerable<TView>>(entities);
        }

        public async Task<TView?> Insertar(TCreateView entidad)
        {
            var entity = _mapper.Map<TEntity>(entidad);
            var result = await _repository.Insertar(entity);
            return result ? _mapper.Map<TView>(entity) : default;
        }

        public async Task<bool> InsertarMasivamente(IEnumerable<TCreateView> entidades)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(entidades);
            return await _repository.InsertarMasivamente(entities);
        }

        public async Task<bool> Actualizar(TUpdateView entidad)
        {
            var entity = _mapper.Map<TEntity>(entidad);
            return await _repository.Actualizar(entity);
        }

        public async Task<bool> ActualizarParcial(TUpdateView entidad, ISet<string> propiedades)
        {
            var entity = _mapper.Map<TEntity>(entidad);
            return await _repository.ActualizarParcial(entity, propiedades);
        }

        public async Task<bool> Eliminar(long codigo) => await _repository.Eliminar(codigo);

        public async Task<bool> EliminarPorCodigoForaneo<TProperty>(string propiedadForanea, TProperty valorForaneo)
        {
            return await _repository.EliminarPorCodigoForaneo(propiedadForanea, valorForaneo);
        }

        public async Task<IEnumerable<TView>> ConsultarPorForanea<TProperty>(string propiedadForanea, TProperty valorForaneo)
        {
            var entities = await _repository.ConsultarPorForanea(propiedadForanea, valorForaneo);
            return _mapper.Map<IEnumerable<TView>>(entities);
        }

        public async Task<bool> Existe(long codigo)
        {
            return await _repository.Existe(codigo);
        }

        public async Task<(List<TProperty> Existentes, List<TProperty> NoExistentes)> ExistenVarios<TProperty>(IEnumerable<TProperty> codigos, string propiedadClave)
        {
            return await _repository.ExistenVarios(codigos, propiedadClave);
        }

        public async Task<(IEnumerable<TView> Items, int TotalRegistros)> ObtenerPorPaginado(int pagina, int tamañoPagina)
        {
            var (entities, totalRegistros) = await _repository.ObtenerPorPaginado(pagina, tamañoPagina);
            var items = _mapper.Map<IEnumerable<TView>>(entities);
            return (items, totalRegistros);
        }

        public async Task<bool> ExistePorForanea<TProperty>(string propiedadForanea, TProperty valorForaneo)
        {
            return await _repository.ExistePorForanea(propiedadForanea, valorForaneo);
        }

        public async Task<IEnumerable<TView>> FiltrarPorPropiedadesAsync(Dictionary<string, object> filtros)
        {
            var entidades = await _repository.FiltrarPorPropiedades(filtros);
            return _mapper.Map<IEnumerable<TView>>(entidades);
        }

        public async Task<(IEnumerable<TView> Items, int TotalRegistros)> FiltrarPorPropiedadesPaginadoAsync(
            Dictionary<string, object> filtros,
            int pageNumber,
            int pageSize)
        {
            var (entities, totalRegistros) = await _repository.FiltrarPorPropiedadesPaginado(
                filtros,
                pageNumber,
                pageSize);
            var items = _mapper.Map<IEnumerable<TView>>(entities);
            return (items, totalRegistros);
        }
    }

    /// <summary>
    /// Variante del servicio base que agrega exportacion Excel usando un filtro propio de la feature.
    /// </summary>
    public class BaseService<TEntity, TView, TCreateView, TUpdateView, TRepository, TExportFilter>(
        TRepository repository,
        IMapper mapper)
        : BaseService<TEntity, TView, TCreateView, TUpdateView, TRepository>(repository, mapper),
          IBaseService<TView, TCreateView, TUpdateView, TExportFilter>
        where TEntity : class
        where TRepository : IBaseRepository<TEntity>
    {
        public virtual async Task<ExcelFileResult> ExportarExcelAsync(TExportFilter filtro)
        {
            var entidades = await ObtenerEntidadesParaExportarAsync(filtro);
            ValidarExportacion(entidades);
            var filas = MapearFilasExportacion(entidades);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(NombreHojaExcel);

            if (filas.Count > 0)
            {
                worksheet.Cell(1, 1).InsertTable(filas, NombreHojaExcel, true);
            }
            else
            {
                var headers = typeof(TView).GetProperties()
                    .Where(property => property.CanRead)
                    .Select(property => property.Name)
                    .ToArray();

                for (var index = 0; index < headers.Length; index++)
                {
                    worksheet.Cell(1, index + 1).Value = headers[index];
                }

                worksheet.Row(1).Style.Font.Bold = true;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return new ExcelFileResult
            {
                FileName = $"{NombreArchivoExcel}-{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx",
                Content = stream.ToArray()
            };
        }

        protected virtual string NombreHojaExcel => typeof(TEntity).Name;
        protected virtual string NombreArchivoExcel => typeof(TEntity).Name.ToLowerInvariant();

        protected virtual async Task<List<TEntity>> ObtenerEntidadesParaExportarAsync(TExportFilter filtro)
        {
            if (filtro is null)
            {
                return await _repository.ObtenerTodos();
            }

            var filtros = filtro.ToFilterDictionary();
            return filtros.Count == 0
                ? await _repository.ObtenerTodos()
                : await _repository.FiltrarPorPropiedades(filtros);
        }

        protected virtual IReadOnlyCollection<TView> MapearFilasExportacion(IEnumerable<TEntity> entidades)
        {
            return _mapper.Map<List<TView>>(entidades);
        }

        protected virtual void ValidarExportacion(IReadOnlyCollection<TEntity> entidades)
        {
        }
    }

    /// <summary>
    /// Variante del servicio base que agrega importacion y plantilla Excel con validacion por fila.
    /// </summary>
    public abstract class BaseService<TEntity, TView, TCreateView, TUpdateView, TRepository, TExportFilter, TImportModel>(
        TRepository repository,
        IMapper mapper,
        IValidator<TImportModel> importValidator,
        IExcelImportSettingsProvider excelImportSettingsProvider)
        : BaseService<TEntity, TView, TCreateView, TUpdateView, TRepository, TExportFilter>(repository, mapper),
          IBaseService<TView, TCreateView, TUpdateView, TExportFilter, TImportModel>
        where TEntity : class
        where TRepository : IBaseRepository<TEntity>
        where TImportModel : class, new()
    {
        private readonly IValidator<TImportModel> _importValidator = importValidator;
        private readonly IExcelImportSettingsProvider _excelImportSettingsProvider = excelImportSettingsProvider;

        public Task<ExcelFileResult> DescargarPlantillaImportacionAsync()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add($"{NombreHojaExcel}_Import");
            EscribirEncabezadosPlantilla(worksheet);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return Task.FromResult(new ExcelFileResult
            {
                FileName = $"{NombreArchivoExcel}-plantilla-importacion.xlsx",
                Content = stream.ToArray()
            });
        }

        public async Task<ImportResult> ImportarExcelAsync(Stream archivo)
        {
            var resultado = new ImportResult();

            using var workbook = new XLWorkbook(archivo);
            var worksheet = workbook.Worksheets.FirstOrDefault();

            if (worksheet is null)
            {
                resultado.Errores.Add("El archivo no contiene una hoja de trabajo valida.");
                return resultado;
            }

            var filas = worksheet.RowsUsed().Skip(1).ToList();
            if (filas.Count == 0)
            {
                resultado.Errores.Add("El archivo no contiene filas de datos para importar.");
                return resultado;
            }

            if (filas.Count > _excelImportSettingsProvider.MaxRowsPerImport)
            {
                resultado.Errores.Add(
                    $"El archivo supera el limite permitido de {_excelImportSettingsProvider.MaxRowsPerImport} filas.");
                return resultado;
            }

            resultado.TotalFilasLeidas = filas.Count;

            var encabezados = ObtenerEncabezados(worksheet);
            var entidades = new List<TEntity>();

            foreach (var fila in filas)
            {
                try
                {
                    var registro = MapearFilaExcel(fila, encabezados);
                    await PrepararFilaImportacionAsync(registro);

                    var validacion = await _importValidator.ValidateAsync(registro);
                    if (!validacion.IsValid)
                    {
                        var errores = string.Join("; ", validacion.Errors.Select(x => x.ErrorMessage));
                        resultado.Errores.Add($"Fila {fila.RowNumber()}: {errores}");
                        continue;
                    }

                    entidades.Add(MapearEntidadImportada(registro));
                }
                catch (Exception ex)
                {
                    resultado.Errores.Add($"Fila {fila.RowNumber()}: {ex.Message}");
                }
            }

            if (entidades.Count == 0)
            {
                return resultado;
            }

            var importado = await _repository.InsertarMasivamente(entidades);
            if (!importado)
            {
                resultado.Errores.Add("No fue posible guardar los registros importados.");
                return resultado;
            }

            resultado.TotalFilasImportadas = entidades.Count;
            return resultado;
        }

        protected virtual Task PrepararFilaImportacionAsync(TImportModel fila)
        {
            return Task.CompletedTask;
        }

        protected abstract TEntity MapearEntidadImportada(TImportModel fila);

        private void EscribirEncabezadosPlantilla(IXLWorksheet worksheet)
        {
            var headers = typeof(TImportModel).GetProperties()
                .Where(property => property.CanRead)
                .Select(property => property.Name)
                .ToArray();

            for (var index = 0; index < headers.Length; index++)
            {
                worksheet.Cell(1, index + 1).Value = headers[index];
            }

            worksheet.Row(1).Style.Font.Bold = true;
            worksheet.Columns().AdjustToContents();
        }

        private static Dictionary<string, int> ObtenerEncabezados(IXLWorksheet worksheet)
        {
            return worksheet.Row(1)
                .CellsUsed()
                .ToDictionary(
                    cell => cell.GetString().Trim(),
                    cell => cell.Address.ColumnNumber,
                    StringComparer.OrdinalIgnoreCase);
        }

        private static TImportModel MapearFilaExcel(IXLRow fila, Dictionary<string, int> encabezados)
        {
            var registro = new TImportModel();
            var propiedades = typeof(TImportModel).GetProperties()
                .Where(propiedad => propiedad.CanWrite)
                .ToList();

            foreach (var propiedad in propiedades)
            {
                if (!encabezados.TryGetValue(propiedad.Name, out var columna))
                {
                    continue;
                }

                var celda = fila.Cell(columna);
                if (celda.IsEmpty())
                {
                    continue;
                }

                var tipoDestino = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;
                object? valor = null;

                if (tipoDestino == typeof(string))
                {
                    valor = celda.GetString().Trim();
                }
                else if (tipoDestino == typeof(DateTime))
                {
                    if (!celda.TryGetValue<DateTime>(out var fecha) &&
                        !DateTime.TryParse(celda.GetString(), out fecha))
                    {
                        throw new InvalidOperationException(
                            $"La columna {propiedad.Name} debe contener una fecha valida.");
                    }

                    valor = fecha;
                }
                else
                {
                    var texto = celda.GetString().Trim();
                    valor = string.IsNullOrWhiteSpace(texto)
                        ? null
                        : Convert.ChangeType(texto, tipoDestino);
                }

                propiedad.SetValue(registro, valor);
            }

            return registro;
        }
    }
}
