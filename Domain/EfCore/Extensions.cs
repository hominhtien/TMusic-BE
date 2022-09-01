using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Domain.EfCore
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomDbContext<TDbContext, TType>(
            this IServiceCollection services,
            string connectionString,
            Action<IServiceProvider, DbContextOptionsBuilder> externalConfig = null)
            where TDbContext : DbContext, IDomainEventContext
        {
            services
                .AddDbContext<TDbContext>((provider, options) =>
                {
                    options.UseNpgsql(connectionString, sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, TimeSpan.FromSeconds(10), null);
                    });

                    // TODO: remove on production
                    options.EnableSensitiveDataLogging();

                    externalConfig?.Invoke(provider, options);
                });

            return services;
        }

        public static void MigrateDataFromScript(this MigrationBuilder migrationBuilder)
        {
            var assembly = Assembly.GetEntryAssembly();
            if (assembly == null)
                return;
            var files = assembly.GetManifestResourceNames();
            var filePrefix = $"{assembly.GetName().Name}.Core.Infrastructure.Persistence.Scripts.";

            foreach (var file in files
                .Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                .Select(f => new { PhysicalFile = f, LogicalFile = f.Replace(filePrefix, string.Empty) })
                .OrderBy(f => f.LogicalFile))
            {
                using var stream = assembly.GetManifestResourceStream(file.PhysicalFile);
                using var reader = new StreamReader(stream!);
                var command = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(command))
                    continue;

                migrationBuilder.Sql(command);
            }
        }
    }
}
