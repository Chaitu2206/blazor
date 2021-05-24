using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog.Extensions.Logging;
using Zindagi.Infra.Options;

namespace Zindagi.Infra.Data
{
    public class ZindagiDbContextDesignFactory : IDesignTimeDbContextFactory<ZindagiDbContext>
    {
        public ZindagiDbContext CreateDbContext(string[] args)
        {
#if DEBUG
            const bool debugging = true;
#else
            const bool debugging = false;
#endif
            var appOptions = new ApplicationOptions
            {
                DataDirectory = "./persistence/",
            };


            var optionsBuilder = new DbContextOptionsBuilder<ZindagiDbContext>()
                .UseSqlite($"Data Source={appOptions.DataDirectory}/zindagi.db;Cache=Shared;", sqlOptions =>
                               sqlOptions.MigrationsHistoryTable(ZindagiDbContext.MIGRATIONS)
                                   .CommandTimeout(120)
                                   .MaxBatchSize(10))
                .EnableDetailedErrors(debugging)
                .EnableSensitiveDataLogging(debugging)
                .UseLazyLoadingProxies()
                .UseSnakeCaseNamingConvention(CultureInfo.InvariantCulture)
                .UseLoggerFactory(new SerilogLoggerFactory());

            return new ZindagiDbContext(optionsBuilder.Options, new NoMediator(), new NullLogger<ZindagiDbContext>());
        }

        private class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification => Task.CompletedTask;

            public Task Publish(object notification, CancellationToken cancellationToken = default) => Task.CompletedTask;

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) => Task.FromResult<TResponse>(default!);

            public Task<object?> Send(object request, CancellationToken cancellationToken = default) => Task.FromResult(default(object));
        }
    }
}
