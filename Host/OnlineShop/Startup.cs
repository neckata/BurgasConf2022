using Host.OnlineShop.ModuleResolver;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Shared.Core.Extensions;
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
                   .AddSerialization(_config)
                   .AddSharedInfrastructure(_config)
                   .AddMediatR(Assembly.GetExecutingAssembly())
                   .AddTransient<IModuleResolver, ModuleResolver.ModuleResolver>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
