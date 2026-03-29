using Microsoft.EntityFrameworkCore;
using Gestion.Ganadera.Infrastructure.Persistence.Interceptors;

namespace Gestion.Ganadera.API.Extensions;

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
}

