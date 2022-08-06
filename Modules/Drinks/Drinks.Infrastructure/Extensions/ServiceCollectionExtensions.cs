using Drinks.Core.Commands;
using Drinks.Core.Interfaces;
using Drinks.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OnlineShop.Modules.Drinks.Infrastructure.Extensions
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
        public static IServiceCollection AddDrinksInfrastructure(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                    });
                    options.MapType<TimeSpan>(() => new OpenApiSchema
                    {
                        Type = "string",
                        Nullable = true,
                        Pattern = @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$",
                        Example = new OpenApiString("02:00:00")
                    });
                });
            services.AddTransient<IDrinksClient, DrinksClient>();
            services.AddMediatR(typeof(GetItemsCommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}