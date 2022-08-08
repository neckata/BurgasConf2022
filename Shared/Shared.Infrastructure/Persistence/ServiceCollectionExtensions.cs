using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Shared.Core.Extensions;
using OnlineShop.Shared.Core.Settings;
using OnlineShop.Shared.Infrastructure.Utilities;
using System.Linq;

namespace OnlineShop.Shared.Infrastructure.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services)
            where T : DbContext
        {
            PersistenceSettings options = services.GetOptions<PersistenceSettings>(nameof(PersistenceSettings));
            string connectionString = options.ConnectionStrings.MSSQL;
            services.AddMSSQL<T>(connectionString);

            return services;
        }

        private static IServiceCollection AddMSSQL<T>(this IServiceCollection services, string connectionString)
            where T : DbContext
        {
            services.AddDbContext<T>(m => m.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(T).Assembly.FullName)));
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();

            var applicationDbContext = dbContext as ApplicationDbContext;
            ModuleTypes.Instance.Modules = applicationDbContext.Modules.ToList();

            return services;
        }
    }
}