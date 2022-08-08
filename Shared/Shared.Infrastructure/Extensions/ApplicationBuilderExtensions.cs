using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Shared.Core.Interfaces.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OnlineShop.Shared.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerDocumentation();
            app.Initialize();
            app.MapModules();
            return app;
        }

        private static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(-1);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
                options.DocExpansion(DocExpansion.None);
            });
            return app;
        }

        private static IApplicationBuilder MapModules(this IApplicationBuilder app)
        {
            string[] directoryPaths = Directory.GetDirectories(@"..\..\Modules");

            foreach (var directoryPath in directoryPaths)
            {
                string moduleName = Path.GetFileName(Path.GetDirectoryName(directoryPath + "\\"));

                Assembly module = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains($"{moduleName}.Infrastructure"));

                Type applicationBuilderExtensions = module.GetTypes().FirstOrDefault(x => x.Name == "ApplicationBuilderExtensions");

                if (applicationBuilderExtensions != null) { 
                    app = (IApplicationBuilder)applicationBuilderExtensions.GetMethod("UseInfrastructure").Invoke(null, new object[] { app });}
            }

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