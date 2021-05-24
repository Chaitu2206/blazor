using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Zindagi.Infra.Data
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase<TDbContext>(this IHost host) where TDbContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
            try
            {
                Log.Information("Database Migration Started.");
                dbContext.Database.Migrate();
                Log.Information("Database Migration Completed.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(MigrationManager));
                throw;
            }

            return host;
        }
    }
}
