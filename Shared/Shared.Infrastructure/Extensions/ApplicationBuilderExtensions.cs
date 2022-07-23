using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Shared.Core.Interfaces.Services;
using OnlineShop.Shared.Infrastructure.Middlewares;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OnlineShop")]

namespace OnlineShop.Shared.Infrastructure.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            app.Initialize();

            return app;
        }

        internal static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializers = serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>();

            foreach (var initializer in initializers)
            {
                initializer.Initialize();
            }

            return app;
        }
    }
}