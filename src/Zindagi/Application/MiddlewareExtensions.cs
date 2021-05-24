using Microsoft.AspNetCore.Builder;

namespace Zindagi.Application
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpsScheme(this IApplicationBuilder app)
        {
            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });
            return app;
        }
    }
}
