using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Gestion.Ganadera.API.Extensions;
using Gestion.Ganadera.API.ErrorHandling;
using Gestion.Ganadera.API.ErrorHandling.Messages;
using Gestion.Ganadera.API.Requests.Helpers;
using Gestion.Ganadera.API.Security.Permissions;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Common.Attributes;
using Gestion.Ganadera.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Application.Features.Base.Models;
using Gestion.Ganadera.Application.Features.Base.ViewModels;

namespace Gestion.Ganadera.API.Controllers.Base
{
    [ApiController]
    /// <summary>
    /// Controller base del template para exponer endpoints CRUD y validaciones comunes.
    /// </summary>
    public abstract class BaseController<TViewModel, TCreateViewModel, TUpdateViewModel>(
        IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel> service, ILogger logger) : ControllerBase
    {
        private const int DefaultPageSize = 25;
        private const int MaxPageSize = 100;

        protected readonly IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel> _service = service;
        protected readonly ILogger _logger = logger;

        [HttpGet("{codigo}")]
        [RequirePermission(ControllerPermission.GetById)]
        public async Task<IActionResult> ConsultarPorCodigo(
            [FromServices] IValidator<CodigoRequest> validator,
            string codigo)
        {
            var validacion = await ControllerValidatorHelper.ValidarCodigo(validator, codigo);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                     HttpContext,
                     validacion
                 );
            }

            var codigoNumerico = codigo.ToLong();
            var existe = await _service.Existe(codigoNumerico);

            if (!existe)
            {
                return ApiProblemDetailsFactory.NotFound(
                     HttpContext,
                     ApiErrorMessages.RecordNotFound(codigoNumerico)
                 );
            }

            var result = await _service.Consultar(codigoNumerico);
            if (result == null)
            {
                return ApiProblemDetailsFactory.NotFound(
                    HttpContext,
                    ApiErrorMessages.RequestedRecordNotFound
                );
            }

            return Ok(result);

        }

        [HttpGet]
        [RequirePermission(ControllerPermission.GetAll)]
        public async Task<IActionResult> ConsultarTodos()
        {
            return Ok(await _service.ObtenerTodos());
        }

        [HttpPost]
        [RequirePermission(ControllerPermission.Create)]
        public async Task<IActionResult> Crear(
            [FromServices] IValidator<TCreateViewModel> validator,
            [FromBody] TCreateViewModel entidad)
        {
            var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, entidad);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                     HttpContext,
                     validacion
                 );
            }

            return await _service.Insertar(entidad)
                ? StatusCode(StatusCodes.Status201Created)
                : ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: ApiErrorMessages.OperationFailed);
        }

        [HttpPost("bulk")]
        [RequirePermission(ControllerPermission.CreateMany)]
        public async Task<IActionResult> CrearMasivo(
            [FromServices] IValidator<TCreateViewModel> validator,
            [FromBody] IEnumerable<TCreateViewModel> entidades)
        {
            var errores = new List<object>();

            foreach (var entidad in entidades)
            {
                var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, entidad);
                if (validacion is not null) errores.Add(validacion);
            }

            if (errores.Count != 0)
            {
                return ApiProblemDetailsFactory.BadRequest(
                      HttpContext,
                      errores
                 );
            }
            else
            {
                return await _service.InsertarMasivamente(entidades)
                    ? StatusCode(StatusCodes.Status201Created)
                    : ApiProblemDetailsFactory.BadRequest(
                        HttpContext,
                        detail: ApiErrorMessages.OperationFailed);
            }
        }

        [HttpPut]
        [RequirePermission(ControllerPermission.Update)]
        public async Task<IActionResult> Actualizar(
            [FromServices] IValidator<TUpdateViewModel> validator,
            [FromBody] TUpdateViewModel entidad)
        {
            var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, entidad);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                       HttpContext,
                       validacion
                  );
            }

            return await _service.Actualizar(entidad)
                ? NoContent()
                : ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: ApiErrorMessages.OperationFailed);
        }

        [HttpPatch("{codigo}")]
        [RequirePermission(ControllerPermission.Update)]
        public async Task<IActionResult> ActualizarParcial(
            [FromServices] IValidator<CodigoRequest> validatorCodigo,
            [FromServices] IValidator<TUpdateViewModel> validatorEntidad,
            string codigo,
            [FromBody] Dictionary<string, JsonElement> body)
        {
            var validacionCodigo = await ControllerValidatorHelper.ValidarCodigo(validatorCodigo, codigo);
            if (validacionCodigo is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(HttpContext, validacionCodigo);
            }

            var codigoNumerico = codigo.ToLong();
            if (!PartialUpdateRequestHelper.TryPreparar<TUpdateViewModel>(
                body,
                codigoNumerico,
                out var entidad,
                out var propiedadesEnviadas,
                out var propiedadCodigo,
                out var error))
            {
                return ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: error);
            }

            var validacionEntidad = await ControllerValidatorHelper.ValidarEntidadParcial(
                validatorEntidad,
                entidad,
                propiedadesEnviadas,
                propiedadCodigo);

            if (validacionEntidad is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(HttpContext, validacionEntidad);
            }

            var existe = await _service.Existe(codigoNumerico);
            if (!existe)
            {
                return ApiProblemDetailsFactory.NotFound(
                    HttpContext,
                    ApiErrorMessages.RecordNotFound(codigoNumerico));
            }

            return await _service.ActualizarParcial(entidad, propiedadesEnviadas)
                ? NoContent()
                : ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: ApiErrorMessages.OperationFailed);
        }

        [HttpDelete("{codigo}")]
        [RequirePermission(ControllerPermission.Delete)]
        public async Task<IActionResult> Eliminar(
            [FromServices] IValidator<CodigoRequest> validator,
            string codigo)
        {
            var validacion = await ControllerValidatorHelper.ValidarCodigo(validator, codigo);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                       HttpContext,
                       validacion
                  );
            }


            var codigoNumerico = codigo.ToLong();
            var existe = await _service.Existe(codigoNumerico);

            if (!existe)
            {
                return ApiProblemDetailsFactory.NotFound(
                    HttpContext,
                    ApiErrorMessages.RecordNotFound(codigoNumerico)
                );
            }

            return await _service.Eliminar(codigoNumerico)
                ? NoContent()
                : ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: ApiErrorMessages.OperationFailed);
        }

        [HttpDelete("foraneo/{valorForaneo}")]
        [RequirePermission(ControllerPermission.DeleteByForeignKey)]
        public async Task<IActionResult> EliminarPorCodigoForaneo(
        [FromServices] IValidator<CodigoRequest> validatorValorForaneo,
        [FromServices] IValidator<PropiedadExistenteRequest> validatorPropiedad,
        string valorForaneo)
        {
            var validacion = await ControllerValidatorHelper.ValidarCodigo(validatorValorForaneo, valorForaneo);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                      HttpContext,
                      validacion
                 );
            }

            var propiedadForanea = typeof(TViewModel)
                .GetProperties()
                .FirstOrDefault(p => Attribute.IsDefined(p, typeof(ForeignKeyDefaultAttribute)))
                ?.Name;

            if (propiedadForanea is null)
                return typeof(TViewModel).ForeignKeyDefaultNotFound();

            var requestPropiedad = new PropiedadExistenteRequest
            {
                Entidad = typeof(TViewModel).FullName ?? string.Empty,
                NombrePropiedad = propiedadForanea
            };

            var validationResultPropiedad =
                await ControllerValidatorHelper.ValidarEntidad(validatorPropiedad, requestPropiedad);

            if (validationResultPropiedad != null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                      HttpContext,
                      validationResultPropiedad
                 );
            }

            var codigoNumerico = valorForaneo.ToLong();
            var existe = await _service.ExistePorForanea(propiedadForanea, codigoNumerico);

            if (!existe)
            {
                return ApiProblemDetailsFactory.NotFound(
                    HttpContext,
                    ApiErrorMessages.RecordNotFound(codigoNumerico)
                );
            }

            return await _service.EliminarPorCodigoForaneo(propiedadForanea, codigoNumerico)
                ? NoContent()
                : ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: ApiErrorMessages.OperationFailed);
        }

        [HttpGet("foraneo/{valorForaneo}")]
        [RequirePermission(ControllerPermission.GetByForeignKey)]
        public async Task<IActionResult> ConsultarPorForanea(
        [FromServices] IValidator<CodigoRequest> validatorValorForaneo,
        [FromServices] IValidator<PropiedadExistenteRequest> validatorPropiedad,
        string valorForaneo)
        {
            var validacion = await ControllerValidatorHelper.ValidarCodigo(validatorValorForaneo, valorForaneo);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                       HttpContext,
                       validacion
                  );
            }

            var propiedadForanea = typeof(TViewModel)
                .GetProperties()
                .FirstOrDefault(p => Attribute.IsDefined(p, typeof(ForeignKeyDefaultAttribute)))
                ?.Name;

            if (propiedadForanea is null)
                return typeof(TViewModel).ForeignKeyDefaultNotFound();

            var requestPropiedad = new PropiedadExistenteRequest
            {
                Entidad = typeof(TViewModel).FullName ?? string.Empty,
                NombrePropiedad = propiedadForanea
            };

            var validationResultPropiedad =
                await ControllerValidatorHelper.ValidarEntidad(validatorPropiedad, requestPropiedad);
            if (validationResultPropiedad != null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                      HttpContext,
                      validationResultPropiedad
                 );
            }

            var codigoNumerico = valorForaneo.ToLong();
            var existe = await _service.ExistePorForanea(propiedadForanea, codigoNumerico);

            if (!existe)
            {
                return ApiProblemDetailsFactory.NotFound(
                   HttpContext,
                   ApiErrorMessages.RecordNotFound(codigoNumerico)
               );
            }

            var result = await _service.ConsultarPorForanea(propiedadForanea, codigoNumerico);
            if (result == null || !result.Any())
            {
                return ApiProblemDetailsFactory.NotFound(
                    HttpContext,
                    ApiErrorMessages.NoRecordsForCriteria
                );
            }

            return Ok(result);
        }

        [HttpGet("existe/{codigo}")]
        [RequirePermission(ControllerPermission.Exists)]
        public async Task<IActionResult> Existe(
        [FromServices] IValidator<CodigoRequest> validator,
        string codigo)
        {
            var validacion = await ControllerValidatorHelper.ValidarCodigo(validator, codigo);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                      HttpContext,
                      validacion
                 );
            }

            return Ok(await _service.Existe(codigo.ToLong()));
        }


        [HttpPost("existen-varios")]
        [RequirePermission(ControllerPermission.ExistsMany)]
        public async Task<IActionResult> ExistenVarios(
        [FromServices] IValidator<ExistenVariosRequest<string>> validator,
        [FromServices] IEntitySchemaMetadata entitySchemaMetadata,
        [FromBody] ExistenVariosRequest<string> request)
        {
            var validacion = await ControllerValidatorHelper.ValidarEntidad(validator, request);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                       HttpContext,
                       validacion
                  );
            }

            var errorPropiedad = BatchExistenceRequestHelper.ValidarPropiedadClave<TViewModel>(
                entitySchemaMetadata,
                request.PropiedadClave);

            if (errorPropiedad is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: errorPropiedad);
            }

            var codigosConvertidos = BatchExistenceRequestHelper.NormalizarCodigos(request.Codigos);

            if (codigosConvertidos.Count == 0)
            {
                return ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: ApiErrorMessages.InvalidNumericCodes
                );
            }

            var (Existentes, NoExistentes) =
                await _service.ExistenVarios(codigosConvertidos, request.PropiedadClave);

            return Ok(new
            {
                Existentes = Existentes.Distinct().ToList(),
                NoExistentes = NoExistentes.Distinct().ToList()
            });
        }

        [HttpGet("existe-foraneo/{valorForaneo}")]
        [RequirePermission(ControllerPermission.ExistsByForeignKey)]
        public async Task<IActionResult> ExistePorForanea(
        [FromServices] IValidator<CodigoRequest> validatorValorForaneo,
        string valorForaneo)
        {
            var validacion = await ControllerValidatorHelper.ValidarCodigo(validatorValorForaneo, valorForaneo);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                       HttpContext,
                       validacion
                  );
            }

            var propiedadForanea = typeof(TViewModel)
                .GetProperties()
                .FirstOrDefault(p => Attribute.IsDefined(p, typeof(ForeignKeyDefaultAttribute)))
                ?.Name;

            if (propiedadForanea is null)
                return typeof(TViewModel).ForeignKeyDefaultNotFound();

            var valorNumerico = valorForaneo.ToLong();
            return Ok(await _service.ExistePorForanea(propiedadForanea, valorNumerico));
        }

        [HttpGet("paginado")]
        [RequirePermission(ControllerPermission.GetPaged)]
        public async Task<IActionResult> ObtenerPorPaginado(
        [FromQuery] int pagina,
        [FromQuery] int tamañoPagina)
        {
            var (items, totalRegistros) =
                await _service.ObtenerPorPaginado(pagina, tamañoPagina);

            return Ok(new { Items = items, TotalRegistros = totalRegistros });
        }


        [HttpPost("filtrar")]
        [RequirePermission(ControllerPermission.Filter)]
        public async Task<IActionResult> Filtrar(
            [FromServices] IValidator<TViewModel> validator,
            [FromBody] Dictionary<string, JsonElement> body)
        {
            if (!FilterRequestHelper.TryPreparar<TViewModel>(
                body,
                out var entidad,
                out var propiedadesEnviadas,
                out var filtros,
                out var error))
            {
                return ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: error);
            }

            var validacion = await ControllerValidatorHelper.ValidarEntidadParcial(
                validator,
                entidad,
                propiedadesEnviadas);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                      HttpContext,
                      validacion
                 );
            }

            var result = await _service.FiltrarPorPropiedadesAsync(filtros);

            if (result.Any())
                return Ok(result);

            var filtrosTexto = string.Join(", ",
                filtros.Select(f => $"{f.Key}={f.Value}")
            );

            return ApiProblemDetailsFactory.NotFound(
                HttpContext,
                ApiErrorMessages.FilterNotFound(filtrosTexto)
            );
        }

        [HttpPost("filtrar-paginado")]
        [RequirePermission(ControllerPermission.Filter)]
        public async Task<IActionResult> FiltrarPaginado(
            [FromServices] IValidator<TViewModel> validator,
            [FromBody] Dictionary<string, JsonElement> body,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            if (!FilterRequestHelper.TryPreparar<TViewModel>(
                body,
                out var entidad,
                out var propiedadesEnviadas,
                out var filtros,
                out var error))
            {
                return ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: error);
            }

            var validacion = await ControllerValidatorHelper.ValidarEntidadParcial(
                validator,
                entidad,
                propiedadesEnviadas);
            if (validacion is not null)
            {
                return ApiProblemDetailsFactory.BadRequest(
                      HttpContext,
                      validacion
                 );
            }

            var pageNumberNormalizado = pageNumber <= 0 ? 1 : pageNumber;
            var pageSizeNormalizado = pageSize <= 0
                ? DefaultPageSize
                : Math.Min(pageSize, MaxPageSize);

            var (items, totalRegistros) = await _service.FiltrarPorPropiedadesPaginadoAsync(
                filtros,
                pageNumberNormalizado,
                pageSizeNormalizado);

            return Ok(new { Items = items, TotalRegistros = totalRegistros });
        }

    }

    /// <summary>
    /// Variante del controller base que agrega capacidades opcionales de Excel sin obligar a todas las features
    /// a implementar importacion o plantilla.
    /// </summary>
    public abstract class BaseController<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter>
        : BaseController<TViewModel, TCreateViewModel, TUpdateViewModel>
        where TExportFilter : class
    {
        private readonly IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter> _excelService;
        private readonly IValidator<TExportFilter> _exportValidator;

        protected BaseController(
            IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter> service,
            ILogger logger,
            IValidator<TExportFilter> exportValidator)
            : base(service, logger)
        {
            _excelService = service;
            _exportValidator = exportValidator;
        }

        [HttpPost("exportar-excel")]
        [RequirePermission(ControllerPermission.ExportExcel)]
        public async Task<IActionResult> ExportarExcel([FromBody] TExportFilter filtro)
        {
            var validacion = await _exportValidator.ValidateAsync(filtro);
            if (!validacion.IsValid)
            {
                return ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    validacion.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
            }

            var archivo = await _excelService.ExportarExcelAsync(filtro);
            return File(archivo.Content, archivo.ContentType, archivo.FileName);
        }
    }

    /// <summary>
    /// Variante del controller base que agrega importacion y plantilla Excel sobre la misma familia base.
    /// </summary>
    public abstract class BaseController<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter, TImportModel>
        : BaseController<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter>
        where TExportFilter : class
        where TImportModel : class, new()
    {
        private readonly IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter, TImportModel> _excelImportService;

        protected BaseController(
            IBaseService<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter, TImportModel> service,
            ILogger logger,
            IValidator<TExportFilter> exportValidator)
            : base(service, logger, exportValidator)
        {
            _excelImportService = service;
        }

        [HttpGet("plantilla-importacion-excel")]
        [RequirePermission(ControllerPermission.DownloadExcelTemplate)]
        public async Task<IActionResult> DescargarPlantillaImportacionExcel()
        {
            var archivo = await _excelImportService.DescargarPlantillaImportacionAsync();
            return File(archivo.Content, archivo.ContentType, archivo.FileName);
        }

        [HttpPost("importar-excel")]
        [Consumes("multipart/form-data")]
        [RequirePermission(ControllerPermission.ImportExcel)]
        public async Task<IActionResult> ImportarExcel([FromForm] Models.Requests.ImportarExcelRequest request)
        {
            var archivo = request.Archivo;

            if (archivo is null || archivo.Length == 0)
            {
                return ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: Requests.Messages.ExcelRequestMessages.FileRequired);
            }

            if (!Path.GetExtension(archivo.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    detail: Requests.Messages.ExcelRequestMessages.FileMustBeXlsx);
            }

            await using var stream = archivo.OpenReadStream();
            var resultado = await _excelImportService.ImportarExcelAsync(stream);

            if (resultado.TotalFilasImportadas == 0 && resultado.Errores.Count > 0)
            {
                return ApiProblemDetailsFactory.BadRequest(
                    HttpContext,
                    resultado.Errores,
                    Requests.Messages.ExcelRequestMessages.NoRowsImported);
            }

            return Ok(resultado);
        }
    }
}



