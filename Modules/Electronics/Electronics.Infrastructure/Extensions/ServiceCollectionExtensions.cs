using Electronics.Core.Commands;
using Electronics.Core.Interfaces;
using Electronics.Core.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
        public static IServiceCollection AddElectronicsInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IElectronicClient, ElectronicClient>();
            services.AddMediatR(typeof(GetItemsCommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}