using GraphQL.Server;
using GraphQL.Types;
using Hectre.BackEnd.Data;
using Hectre.BackEnd.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace Hectre.BackEnd.Common
{
    public static class GraphBuilderExtension
    {
        public static IGraphQLBuilder AddAllGraphType(this IGraphQLBuilder builder,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            var assembly = Assembly.GetCallingAssembly();
            // Register all GraphQL types
            foreach (var type in assembly.GetTypes()
                .Where(x => !x.IsAbstract && !x.IsGenericType && typeof(IGraphType).IsAssignableFrom(x)))
            {
                try
                {
                    //Console.WriteLine(type.FullName);
                    builder.Services.TryAdd(new ServiceDescriptor(type, type, serviceLifetime));
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return builder;
        }

        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {

            var allEntityTypes = AssemblyUtils.GetTypesDerivedFromType(Assembly.GetExecutingAssembly(), typeof(BaseEntity));
            var dbContextType = typeof(HectreDbContext);

            foreach (var entityType in allEntityTypes)
            {
                var gInterfaceType = typeof(IEfRepository<,>);
                var gImplementType = typeof(EFRepository<,>);
                Type[] typeArgs = { entityType, dbContextType };

                var genericInterfaceType = gInterfaceType.MakeGenericType(typeArgs);
                var genericImplementType = gImplementType.MakeGenericType(typeArgs);

                services.AddScoped(genericInterfaceType, genericImplementType);
            }

            return services;
        }
    }


}
