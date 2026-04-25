using System.Collections.ObjectModel;
using System.Data;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Gestion.Ganadera.Business.API.Extensions
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
                var explicitApiCodigo = Environment.GetEnvironmentVariable("ApiInfo__Codigo");
                var azureSiteName = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");
                var apiCodigo =
                    !string.IsNullOrWhiteSpace(explicitApiCodigo) ? explicitApiCodigo :
                    !string.IsNullOrWhiteSpace(azureSiteName) ? azureSiteName :
                    context.Configuration["ApiInfo:Codigo"] ?? "Gestion.Ganadera.Business.API";
                var columnOptions = new ColumnOptions
                {
                    AdditionalColumns = new Collection<SqlColumn>
                    {
                        new()
                        {
                            ColumnName = "Log_Aplicacion_Api_Codigo",
                            DataType = SqlDbType.NVarChar,
                            DataLength = 100,
                            AllowNull = true
                        },
                        new()
                        {
                            ColumnName = "Log_Aplicacion_Origen",
                            PropertyName = "Log_Aplicacion_Origen",
                            DataType = SqlDbType.NVarChar,
                            AllowNull = false
                        },
                        new()
                        {
                            ColumnName = "Log_Aplicacion_Usuario",
                            DataType = SqlDbType.NVarChar,
                            DataLength = 200,
                            AllowNull = true
                        },
                        new()
                        {
                            ColumnName = "Log_Aplicacion_CorrelationId",
                            PropertyName = "CorrelationId",
                            DataType = SqlDbType.NVarChar,
                            DataLength = 100,
                            AllowNull = true
                        },
                        new()
                        {
                            ColumnName = "Cliente_Codigo",
                            DataType = SqlDbType.BigInt,
                            AllowNull = true
                        }
                    }
                };

                columnOptions.Id.ColumnName = "Log_Aplicacion_Codigo";
                columnOptions.Message.ColumnName = "Log_Aplicacion_Mensaje";
                columnOptions.Level.ColumnName = "Log_Aplicacion_Nivel";
                columnOptions.TimeStamp.ColumnName = "Log_Aplicacion_Fecha";
                columnOptions.Exception.ColumnName = "Log_Aplicacion_Excepcion";

                columnOptions.Store.Remove(StandardColumn.MessageTemplate);
                columnOptions.Store.Remove(StandardColumn.Properties);

                config
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)
                    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("CorrelationId", string.Empty)
                    .Enrich.WithProperty("Log_Aplicacion_Api_Codigo", apiCodigo)
                    .Enrich.WithProperty("Log_Aplicacion_Origen", "API")
                    .Enrich.WithProperty("Application", "Gestion.Ganadera.Business.API")
                    .WriteTo.Console()
                    .WriteTo.MSSqlServer(
                        connectionString: context.Configuration.GetConnectionString("DefaultConnection"),
                        sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "Log_Aplicacion",
                            SchemaName = "Seguridad",
                            AutoCreateSqlTable = false
                        },
                        columnOptions: columnOptions,
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information);
            });

            return builder;
        }
    }
}

