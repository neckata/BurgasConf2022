using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OnlineShop.Shared.Core.IntegrationServices.Module;
using OnlineShop.Shared.Core.Interfaces;
using OnlineShop.Shared.Core.Interfaces.Services;
using OnlineShop.Shared.Core.Interfaces.Services.Module;
using OnlineShop.Shared.Core.Settings;
using OnlineShop.Shared.Infrastructure.Persistence;
using OnlineShop.Shared.Infrastructure.Utilities;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OnlineShop.Shared.Infrastructure.Extensions
{
    /// <summary>
    /// Extends ServiceCollection and add all shared infrastructure
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddPersistenceSettings(config);
            services
                .AddDatabaseContext<ApplicationDbContext>()
                .AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddControllers();
            services.AddApplicationLayer(config);
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddTransient<IDatabaseSeeder, ModuleDbSeeder>();
            services.AddSwaggerDocumentation();

            services.MapModules(config);

            return services;
        }

        /// <summary>
        /// Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IModuleService, ModuleService>();
            return services;
        }

        private static IServiceCollection AddPersistenceSettings(this IServiceCollection services, IConfiguration config)
        {
            return services
                .Configure<PersistenceSettings>(config.GetSection(nameof(PersistenceSettings)));
        }

        private static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.AddSwaggerDocs();
            });
        }

        private static void AddSwaggerDocs(this SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "BurgasConf2022 API"
            });
        }

        /// <summary>
        /// Get all modules in folder "Modules" and load their dlls to the main assembly
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection MapModules(this IServiceCollection services, IConfiguration config)
        {
            List<Assembly> loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            string[] loadedPaths = loadedAssemblies.Where(p => !p.IsDynamic && p.GetName().Name != "OnlineShop").Select(a => a.GetName().Name).OrderBy(x => x).ToArray();

            string[] directoryPaths = Directory.GetDirectories(@"..\..\Modules");

            List<Core.Entities.Module> dbModules = ModuleTypes.Instance.Modules;

            List<string> modules = new List<string>();

            foreach (var directoryPath in directoryPaths)
            {
                string moduleName = Path.GetFileName(Path.GetDirectoryName(directoryPath + "\\"));

                var moduleInDB = dbModules.FirstOrDefault(x => x.Name == moduleName);
                if (moduleInDB == null || (moduleInDB != null && moduleInDB.IsActive))
                {
                    modules.Add(moduleName);

                    string buildFolder = System.IO.Directory.GetDirectories($@"{directoryPath}\{moduleName}.Infrastructure\bin\Debug\")[0];

                    string[] referencedPaths = Directory.GetFiles(buildFolder, "*.dll");

                    List<string> toLoad = new List<string>();

                    foreach (string path in referencedPaths)
                    {
                        bool add = true;
                        foreach (var loadedPath in loadedPaths)
                        {
                            if (path.Contains(loadedPath))
                            {
                                add = false;
                                break;
                            }
                        }

                        if (add)
                            toLoad.Add(path);
                    }

                    toLoad.ForEach(filename =>
                    {
                        Assembly a = Assembly.LoadFrom(Path.GetFullPath(filename));
                        AppDomain.CurrentDomain.Load(a.GetName());

                        services.AddMvc().AddApplicationPart(a).AddControllersAsServices();

                    });

                    Assembly module = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains($"{moduleName}.Infrastructure"));

                    Type serviceCollectionExtensions = module.GetTypes().First(x => x.Name == "ServiceCollectionExtensions");

                    services = (IServiceCollection)serviceCollectionExtensions.GetMethod("AddInfrastructure").Invoke(null, new object[] { services, config });
                }
            }

            ModuleTypes.Instance.Modules = modules.Select(x => new Core.Entities.Module { Name = x }).ToList();

            return services;
        }
    }
}