using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineShop.Shared.Core.Extensions
{
    /// <summary>
    /// Extends ServiceCollection and adds Serialization
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static T GetOptions<T>(this IServiceCollection services, string sectionName)
            where T : new()
        {
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IConfigurationSection section = configuration.GetSection(sectionName);
            var options = new T();
            section.Bind(options);

            return options;
        }
    }
}