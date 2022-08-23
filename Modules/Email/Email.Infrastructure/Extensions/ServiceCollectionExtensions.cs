using Email.Core.Commands;
using Email.Core.Interfaces;
using Email.Core.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OnlineShop.Modules.Email.Infrastructure.Extensions
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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddMediatR(typeof(EmailHandler).GetTypeInfo().Assembly);
            return services;
        }
    }
}