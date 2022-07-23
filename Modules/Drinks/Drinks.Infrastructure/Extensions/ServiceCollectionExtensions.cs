using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Drinks.Core.Commands;
using Drinks.Core.Interfaces;
using Drinks.Core.Services;
using System.Reflection;

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
        public static IServiceCollection AddDrinksInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IDrinksClient, DrinksClient>();
            services.AddMediatR(typeof(CreateActionCommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}