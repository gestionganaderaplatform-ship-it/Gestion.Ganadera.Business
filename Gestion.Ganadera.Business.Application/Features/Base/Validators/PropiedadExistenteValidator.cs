using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Gestion.Ganadera.Business.Application.Common.Constants;
using Gestion.Ganadera.Business.Application.Common.Messages;
using Gestion.Ganadera.Business.Application.Features.Base.Models;

namespace Gestion.Ganadera.Business.Application.Features.Base.Validators
{
    /// <summary>
    /// Verifica que una propiedad exista en el modelo y pueda usarse en operaciones genericas del template.
    /// </summary>
    public class PropiedadExistenteValidator : AbstractValidator<PropiedadExistenteRequest>
    {
        public PropiedadExistenteValidator()
        {
            RuleFor(x => x.NombrePropiedad)
             .NotEmpty().WithMessage(ValidationMessages.PropertyNameRequired)
             .Must((x, propiedad) => ExistePropiedad(x.Entidad, propiedad))
             .WithMessage(x => ValidationMessages.PropertyNotFound(x.NombrePropiedad, x.Entidad))
             .Matches(RegexPatterns.SoloLetrasConGuionBajo)
             .WithMessage(ValidationMessages.PropertyNameInvalidFormat);
        }

        private static bool ExistePropiedad(string entidad, string propiedadForanea)
        {
            var tipoEntidad = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.FullName == entidad);

            if (tipoEntidad == null)
            {
                return false;
            }

            var propiedad = tipoEntidad.GetProperty(propiedadForanea,
                BindingFlags.Public | BindingFlags.Instance);

            return propiedad != null &&
                   !Attribute.IsDefined(propiedad, typeof(NotMappedAttribute));
        }
    }
}