using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Gestion.Ganadera.Infrastructure.Persistence
{
    /// <summary>
    /// Crea el DbContext en tiempo de diseno para que las herramientas EF Core funcionen sin depender del host de la API.
    /// </summary>
    public sealed class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = ResolveConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(
                connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name));

            return new AppDbContext(optionsBuilder.Options);
        }

        private static string ResolveConnectionString()
        {
            var envConnectionString = Environment.GetEnvironmentVariable("EF_CONNECTION_STRING");
            if (!string.IsNullOrWhiteSpace(envConnectionString))
            {
                return envConnectionString;
            }

            var apiDirectory = ResolveApiDirectory();

            foreach (var settingsFile in new[] { "appsettings.Development.json", "appsettings.json" })
            {
                var appSettingsPath = Path.Combine(apiDirectory, settingsFile);

                if (File.Exists(appSettingsPath))
                {
                    using var stream = File.OpenRead(appSettingsPath);
                    using var document = JsonDocument.Parse(stream);

                    if (document.RootElement.TryGetProperty("ConnectionStrings", out var connectionStrings) &&
                        connectionStrings.TryGetProperty("DefaultConnection", out var defaultConnection) &&
                        !string.IsNullOrWhiteSpace(defaultConnection.GetString()))
                    {
                        return defaultConnection.GetString()!;
                    }
                }
            }

            var userSecretsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Microsoft",
                "UserSecrets",
                "c9ca252f-a6f7-4532-8896-a44a2a55b628",
                "secrets.json");

            if (File.Exists(userSecretsPath))
            {
                using var stream = File.OpenRead(userSecretsPath);
                using var document = JsonDocument.Parse(stream);

                if (document.RootElement.TryGetProperty("ConnectionStrings", out var connectionStrings) &&
                    connectionStrings.TryGetProperty("DefaultConnection", out var defaultConnection) &&
                    !string.IsNullOrWhiteSpace(defaultConnection.GetString()))
                {
                    return defaultConnection.GetString()!;
                }
            }

            throw new InvalidOperationException(
                "No se encontro ConnectionStrings:DefaultConnection. Configure EF_CONNECTION_STRING o user secrets.");
        }

        private static string ResolveApiDirectory()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var candidates = new[]
            {
                currentDirectory,
                Path.Combine(currentDirectory, "Gestion.Ganadera.API"),
                Path.GetFullPath(Path.Combine(currentDirectory, "..", "Gestion.Ganadera.API"))
            };

            foreach (var candidate in candidates.Distinct(StringComparer.OrdinalIgnoreCase))
            {
                if (File.Exists(Path.Combine(candidate, "appsettings.json")))
                {
                    return candidate;
                }
            }

            throw new DirectoryNotFoundException(
                "No fue posible ubicar la carpeta Gestion.Ganadera.API para resolver la configuracion de EF Core.");
        }
    }
}
