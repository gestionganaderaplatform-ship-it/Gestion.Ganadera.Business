using FluentValidation;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Common.Constants;
using Gestion.Ganadera.Application.Common.Extensions;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Interfaces;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Messages;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.ViewModels;

namespace Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Validators
{
    public class AuditoriaCreateValidator(IEntityValidationMetadata metadata)
        : StandardEntityValidator<AuditoriaCreateViewModel>(metadata)
    {
    }

    public class AuditoriaUpdateValidator(IEntityValidationMetadata metadata)
        : StandardEntityValidator<AuditoriaUpdateViewModel>(metadata)
    {
    }

    public class AuditoriaViewValidator : StandardEntityValidator<AuditoriaViewModel>
    {
        private readonly IAuditoriaService _service;

        public AuditoriaViewValidator(
            IEntityValidationMetadata metadata,
            IAuditoriaService service)
            : base(metadata, vm => vm.ToFilterDictionary().Keys.ToHashSet())
        {
            _service = service;

            When(x => x.Auditoria_Codigo > 0, () =>
            {
                RuleFor(x => x.Auditoria_Codigo)
                    .MustAsync(async (codigo, _) => await _service.Existe(codigo))
                    .WithMessage(AuditoriaValidationMessages.AuditNotFound);
            });

            RuleFor(x => x.Auditoria_Fecha_Modificado)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage(AuditoriaValidationMessages.ModifiedDateCannotBeFuture);

            When(x => !string.IsNullOrWhiteSpace(x.Auditoria_Modificado_Por), () =>
            {
                RuleFor(x => x.Auditoria_Modificado_Por)
                    .Matches(RegexPatterns.IdentificadorActor)
                    .WithMessage(AuditoriaValidationMessages.ActorIdentifierInvalidFormat);
            });
        }
    }

    public class AuditoriaExportFilterValidator : StandardEntityValidator<AuditoriaExportFilterViewModel>
    {
        private const int MaxExportRangeDays = 90;
        private const int MaxExportRecords = 10_000;
        private readonly IAuditoriaService _service;

        public AuditoriaExportFilterValidator(
            IEntityValidationMetadata metadata,
            IAuditoriaService service)
            : base(metadata, vm => vm.ToFilterDictionary().Keys.ToHashSet())
        {
            _service = service;

            RuleFor(x => x.Auditoria_Fecha_Modificado_Desde)
                .NotNull()
                .WithMessage(AuditoriaValidationMessages.ExportDateFromRequired);

            RuleFor(x => x.Auditoria_Fecha_Modificado_Hasta)
                .NotNull()
                .WithMessage(AuditoriaValidationMessages.ExportDateToRequired);

            When(x => !string.IsNullOrWhiteSpace(x.Auditoria_Modificado_Por), () =>
            {
                RuleFor(x => x.Auditoria_Modificado_Por)
                    .Matches(RegexPatterns.IdentificadorActor)
                    .WithMessage(AuditoriaValidationMessages.ActorIdentifierInvalidFormat);
            });

            When(
                x => x.Auditoria_Fecha_Modificado_Desde.HasValue &&
                     x.Auditoria_Fecha_Modificado_Hasta.HasValue,
                () =>
            {
                RuleFor(x => x.Auditoria_Fecha_Modificado_Hasta)
                    .GreaterThanOrEqualTo(x => x.Auditoria_Fecha_Modificado_Desde!.Value)
                    .WithMessage(AuditoriaValidationMessages.InvalidModifiedDateRange);

                RuleFor(x => x)
                    .Must(x =>
                        x.Auditoria_Fecha_Modificado_Hasta!.Value.Date
                        .Subtract(x.Auditoria_Fecha_Modificado_Desde!.Value.Date)
                        .TotalDays <= MaxExportRangeDays)
                    .WithMessage(AuditoriaValidationMessages.ExportDateRangeExceeded);

                RuleFor(x => x)
                    .MustAsync(EstarDentroDelLimiteDeExportacionAsync)
                    .WithMessage(AuditoriaValidationMessages.ExportRecordLimitExceeded(MaxExportRecords));
            });
        }

        private async Task<bool> EstarDentroDelLimiteDeExportacionAsync(
            AuditoriaExportFilterViewModel filtro,
            CancellationToken cancellationToken)
        {
            var filtros = new Dictionary<string, object>
            {
                [nameof(AuditoriaViewModel.Auditoria_Fecha_Modificado)] = filtro.Auditoria_Fecha_Modificado_Desde!.Value
            };

            if (!string.IsNullOrWhiteSpace(filtro.Auditoria_Nombre_Tabla))
            {
                filtros[nameof(AuditoriaViewModel.Auditoria_Nombre_Tabla)] = filtro.Auditoria_Nombre_Tabla.Trim();
            }

            if (!string.IsNullOrWhiteSpace(filtro.Auditoria_Modificado_Por))
            {
                filtros[nameof(AuditoriaViewModel.Auditoria_Modificado_Por)] = filtro.Auditoria_Modificado_Por.Trim();
            }

            var entidades = await _service.FiltrarPorPropiedadesAsync(filtros);

            var total = entidades
                .Where(x => x.Auditoria_Fecha_Modificado <= filtro.Auditoria_Fecha_Modificado_Hasta!.Value)
                .Take(MaxExportRecords + 1)
                .Count();

            return total <= MaxExportRecords;
        }
    }
}
