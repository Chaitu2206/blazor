#define SQLite
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Extensions.Logging;
using StackExchange.Redis;
using Zindagi.Domain.RequestsAggregate;
using Zindagi.Domain.UserAggregate;
using Zindagi.Infra.App;
using Zindagi.Infra.BackgroundJobs;
using Zindagi.Infra.Data;
using Zindagi.Infra.JsonConverters;
using Zindagi.Infra.Options;
using Zindagi.Infra.Repositories;
using Zindagi.SeedWork;


namespace Zindagi.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration config)
        {
            var appOptions = new ApplicationOptions();
            config.Bind(ApplicationOptions.AppSettingsSection, appOptions);
            services.AddSingleton(appOptions);

            services.AddDbContextFactory<ZindagiDbContext>(options =>
            {
#if SQLite
                options.UseSqlite($"Data Source={appOptions.DataDirectory}/zindagi.db", sqlOptions =>
                                      sqlOptions.MigrationsHistoryTable(ZindagiDbContext.MIGRATIONS)
                                          .CommandTimeout(120)
                                          .MaxBatchSize(10));
#else

                options.UseNpgsql(config.GetConnectionString("sql"), sqlOptions =>
                                      sqlOptions.MigrationsHistoryTable(ZindagiDbContext.MIGRATIONS)
                                          .CommandTimeout(120)
                                          .MaxBatchSize(10));
#endif
                options
                    .EnableDetailedErrors(config.IsDevelopment())
                    .EnableSensitiveDataLogging(config.IsDevelopment())
                    .UseLazyLoadingProxies()
                    .UseSnakeCaseNamingConvention(CultureInfo.InvariantCulture)
                    .UseLoggerFactory(new SerilogLoggerFactory());
            }, ServiceLifetime.Transient);


                services.AddScoped(p => p.GetRequiredService<IDbContextFactory<ZindagiDbContext>>().CreateDbContext());

            var redisOptions = ConfigurationOptions.Parse(config.GetConnectionString("redis"));
            services.AddSingleton(redisOptions);
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisOptions));

            services.AddSignalR().AddStackExchangeRedis(config.GetConnectionString("redis"));

            var jsonOptions = new JsonSerializerOptions
            {
                AllowTrailingCommas = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                ReadCommentHandling = JsonCommentHandling.Skip
            };
            jsonOptions.Converters.Add(new StatusConverter());
            jsonOptions.Converters.Add(new BloodGroupConverter());

            services.AddSingleton(jsonOptions);

            var smtpOptions = new SmtpOptions();
            config.Bind(SmtpOptions.AppSettingsSection, smtpOptions);
            services.AddSingleton(smtpOptions);

            var smsOptions = new SmsOptions();
            config.Bind(SmsOptions.AppSettingsSection, smsOptions);
            services.AddSingleton(smsOptions);



            services.AddHostedService<NewRequestProcessingService>();

            // Singleton: creates a new instance only once during the application lifetime
            // Scoped: creates a new instance for every request
            // Transient: creates a new instance every time you request it

            services.AddScoped<ILoggedInUser, LoggedInUser>();

            services.AddSingleton<IMessaging, Messaging>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBloodRequestRepository, BloodRequestRepository>();

            services.AddTransient<IBloodRequestsSearchRepository, BloodRequestsSearchRepository>();
        }
    }
}
