using System.Collections.ObjectModel;
using System.Data;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Gestion.Ganadera.API.Extensions
{
    /// <summary>
    /// Configura logging estructurado del API y su salida a consola y base de datos.
    /// </summary>
    public static class LoggingExtensions
    {
        public static WebApplicationBuilder AddLogging(
            this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, services, config) =>
            {
                var apiCodigo = context.Configuration["ApiInfo:Codigo"] ?? "Gestion.Ganadera.API";
                var columnOptions = new ColumnOptions
                {
                    AdditionalColumns = new Collection<SqlColumn>
                    {
                        new()
                        {
                            ColumnName = "Log_Aplicacion_Api_Codigo",
                            DataType = SqlDbType.NVarChar,
                            DataLength = 100
                        }
                    }
                };

                config
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)
                    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Log_Aplicacion_Api_Codigo", apiCodigo)
                    .Enrich.WithProperty("Application", "Gestion.Ganadera.API")
                    .WriteTo.Console()
                    .WriteTo.MSSqlServer(
                        connectionString: context.Configuration.GetConnectionString("DefaultConnection"),
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "Log_Aplicacion",
                            SchemaName = "Seguridad",
                            AutoCreateSqlTable = true
                        },
                        columnOptions: columnOptions,
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information);
            });

            return builder;
        }
    }
}

