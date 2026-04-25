namespace Gestion.Ganadera.Business.API.Extensions
{
    /// <summary>
    /// Registra perfiles de AutoMapper requeridos por las features del template.
    /// </summary>
    public static class ConfigureAutoMapperExtensions
    {
        public static WebApplicationBuilder AddAutoMapper(
            this WebApplicationBuilder builder)
        {
            var licenseKey = builder.Configuration["AutoMapper:LicenseKey"];

            builder.Services.AddAutoMapper(cfg =>
            {
                if (!string.IsNullOrWhiteSpace(licenseKey))
                {
                    cfg.LicenseKey = licenseKey;
                }
            }, AppDomain.CurrentDomain.GetAssemblies());

            return builder;
        }
    }
}
