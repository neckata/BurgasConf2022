using Drinks.Core.Interfaces;
using Drinks.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineShop.Modules.Drinks.Infrastructure.Extensions
{
    /// <summary>
    /// Extension of ServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Called by reflection in Startup, injecting all services in main assembly
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDrinksService, DrinksService>();
            return services;
        }
    }
}