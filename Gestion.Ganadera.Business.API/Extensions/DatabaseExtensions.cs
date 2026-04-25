using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Interceptors;

namespace Gestion.Ganadera.Business.API.Extensions;

/// <summary>
/// Registra el DbContext principal y su configuracion de acceso a SQL Server.
/// </summary>
public static class DatabaseExtensions
{
    public static WebApplicationBuilder AddSqlServerDatabase<TDbContext>(
        this WebApplicationBuilder builder)
        where TDbContext : DbContext
    {


        builder.Services.AddScoped<AuditSaveChangesInterceptor>();

        builder.Services.AddDbContext<TDbContext>((sp, options) =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });

            options.AddInterceptors(
                sp.GetRequiredService<AuditSaveChangesInterceptor>()
            );
        });
        return builder;
    }

    /// <summary>
    /// Aplica migraciones automaticas solo en desarrollo local.
    /// En ambientes compartidos o de despliegue se debe usar script idempotente previo.
    /// </summary>
    public static async Task ApplySafeDevelopmentMigrationsAsync<TDbContext>(
        this WebApplication app)
        where TDbContext : DbContext
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        await using var scope = app.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<TDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TDbContext>>();
        var forceAutoMigrate = app.Configuration.GetValue<bool>("Database:ForceAutoMigrate");

        if (!await db.Database.CanConnectAsync())
        {
            await db.Database.MigrateAsync();
            return;
        }

        if (forceAutoMigrate)
        {
            logger.LogWarning(
                "Se forzara Database.Migrate para {DbContext} porque Database:ForceAutoMigrate esta habilitado en {EnvironmentName}.",
                typeof(TDbContext).Name,
                app.Environment.EnvironmentName);
            await db.Database.MigrateAsync();
            return;
        }

        var migrationsAssembly = db.Database.GetService<IMigrationsAssembly>();
        var availableMigrations = migrationsAssembly.Migrations.Keys.ToHashSet(StringComparer.Ordinal);
        var appliedMigrations = (await db.Database.GetAppliedMigrationsAsync()).ToArray();
        var unknownAppliedMigrations = appliedMigrations
            .Where(migrationId => !availableMigrations.Contains(migrationId))
            .ToArray();

        if (unknownAppliedMigrations.Length > 0)
        {
            logger.LogWarning(
                "Se omite Database.Migrate para {DbContext} porque la base contiene migraciones no presentes en el ensamblado actual: {MigrationIds}",
                typeof(TDbContext).Name,
                string.Join(", ", unknownAppliedMigrations));
            return;
        }

        if (appliedMigrations.Length == 0 && await HasUnexpectedUserTablesAsync(db))
        {
            logger.LogWarning(
                "Se omite Database.Migrate para {DbContext} porque la base ya tiene tablas de usuario pero no registra historial de migraciones.",
                typeof(TDbContext).Name);
            return;
        }

        await db.Database.MigrateAsync();
    }

    private static async Task<bool> HasUnexpectedUserTablesAsync(DbContext dbContext)
    {
        var connection = dbContext.Database.GetDbConnection();
        var shouldCloseConnection = connection.State != ConnectionState.Open;

        if (shouldCloseConnection)
        {
            await connection.OpenAsync();
        }

        try
        {
            var userTables = new List<string>();

            await using var command = connection.CreateCommand();
            command.CommandText =
                """
                SELECT TABLE_SCHEMA, TABLE_NAME
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_TYPE = 'BASE TABLE'
                  AND TABLE_NAME <> '__EFMigrationsHistory'
                """;

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                userTables.Add($"{reader.GetString(0)}.{reader.GetString(1)}");
            }

            if (userTables.Count == 0)
            {
                return false;
            }

            var ignorableBootstrapTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Seguridad.Log_Aplicacion"
            };

            return userTables.Any(tableName => !ignorableBootstrapTables.Contains(tableName));
        }
        finally
        {
            if (shouldCloseConnection)
            {
                await connection.CloseAsync();
            }
        }
    }
}

