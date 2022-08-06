using Host.OnlineShopWebAPI.ModuleResolver;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Shared.Infrastructure.Extensions;
using System.Reflection;

namespace OnlineShopWebAPI
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache()
              .AddSharedInfrastructure(_config)
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient<IModuleResolver, ModuleResolver>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure();
        }
    }
}
