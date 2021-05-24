using System.Globalization;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zindagi.Application.Behaviors;
using Zindagi.Infra.Auth;

namespace Zindagi.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApp(this IServiceCollection services, IConfiguration config)
        {
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.IsEssential = true;
                options.Secure = CookieSecurePolicy.Always;
                options.HttpOnly = HttpOnlyPolicy.Always;
            });

            services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);

            services.Configure<KestrelServerOptions>(options => options.AddServerHeader = false);

            services.AddAutoMapper(DomainExtensions.Assembly(), InfraExtensions.Assembly(), Extensions.Assembly());

            services.AddMediatR(DomainExtensions.Assembly(), InfraExtensions.Assembly(), Extensions.Assembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            //// services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //// services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssemblies(new[] { DomainExtensions.Assembly(), InfraExtensions.Assembly(), Extensions.Assembly() });

            services.AddAuth0(config);
        }
    }
}
