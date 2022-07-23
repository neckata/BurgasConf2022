using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Food.Core.Commands;
using Food.Core.Interfaces;
using Food.Core.Services;
using System.Reflection;

namespace OnlineShop.Modules.Food.Infrastructure.Extensions
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
        public static IServiceCollection AddFoodInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IFoodClient, FoodClient>();
            services.AddMediatR(typeof(CreateActionCommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}