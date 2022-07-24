using Host.OnlineShop.ModuleResolver;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Shared.Infrastructure.Extensions;
using System.Reflection;

namespace Host.OnlineShop
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                   .AddDistributedMemoryCache()
                   .AddSharedInfrastructure(_config)
                   .AddMediatR(Assembly.GetExecutingAssembly())
                   .AddTransient<IModuleResolver, ModuleResolver.ModuleResolver>();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure();
        }
    }
}
