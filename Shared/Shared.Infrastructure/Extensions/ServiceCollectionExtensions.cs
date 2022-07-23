using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Shared.Core.Exceptions;
using OnlineShop.Shared.Core.Extensions;
using OnlineShop.Shared.Core.IntegrationServices.Module;
using OnlineShop.Shared.Core.Interfaces;
using OnlineShop.Shared.Core.Interfaces.Services.Module;
using OnlineShop.Shared.Core.Settings;
using OnlineShop.Shared.Infrastructure.Interceptors;
using OnlineShop.Shared.Infrastructure.Middlewares;
using OnlineShop.Shared.Infrastructure.Persistence;
using OnlineShop.Shared.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OnlineShop")]

namespace OnlineShop.Shared.Infrastructure.Extensions
{
    /// <summary>
    /// Extends ServiceCollection and add all shared infrastructure
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddPersistenceSettings(config);
            services
                .AddDatabaseContext<ApplicationDbContext>()
                .AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddControllers()
                .AddMvcOptions(options =>
                {
                    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, propertyName) =>
                        throw new CustomException($"{propertyName}: value '{value}' is invalid.", statusCode: HttpStatusCode.BadRequest));
                });
            services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();
            services.AddApplicationLayer(config);
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSingleton<GlobalExceptionHandler>();
            services.AddCorsPolicy();
            services.AddApplicationSettings(config);
            services.MapModules();

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

        private static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)));
        }

        private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            var corsSettings = services.GetOptions<CorsSettings>(nameof(CorsSettings));
            return services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(corsSettings.Url);
                });
            });
        }

        /// <summary>
        /// Get all modules in folder "Modules" and load their dlls to the main assembly
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection MapModules(this IServiceCollection services)
        {
            List<Assembly> loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            string[] loadedPaths = loadedAssemblies.Where(p => !p.IsDynamic && p.GetName().Name != "OnlineShop").Select(a => a.GetName().Name).OrderBy(x => x).ToArray();

            string[] directoryPaths = Directory.GetDirectories(@"..\..\Modules");

            List<string> modules = new List<string>();

            foreach (var directoryPath in directoryPaths)
            {
                string moduleName = Path.GetFileName(Path.GetDirectoryName(directoryPath + "\\"));
                modules.Add(moduleName);

                string[] referencedPaths = Directory.GetFiles($@"{directoryPath}\{moduleName}.Infrastructure\bin\Debug\net5.0", "*.dll");

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

                ModuleTypes.Instance.Modules = modules;

                toLoad.ForEach(filename =>
                {
                    Assembly a = Assembly.LoadFrom(Path.GetFullPath(filename));
                    AppDomain.CurrentDomain.Load(a.GetName());
                });

                Assembly module = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains($"{moduleName}.Infrastructure"));

                Type serviceCollectionExtensions = module.GetTypes().First(x => x.Name == "ServiceCollectionExtensions");

                services = (IServiceCollection)serviceCollectionExtensions.GetMethod($"Add{moduleName}Infrastructure").Invoke(null, new object[] { services });
            }

            return services;
        }
    }
}