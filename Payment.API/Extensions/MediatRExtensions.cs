﻿using Payment.API.Behaviors;
using System.Reflection;

namespace Payment.API.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services,
           Assembly assembly)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                //config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            return services;
        }
    }
}
