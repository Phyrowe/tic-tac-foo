using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TicTacFoo.Application.Common.Attributes;

namespace TicTacFoo.Application.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServicesByAttribute(this IServiceCollection services)
        {
            RegisterWithAttribute(ref services, typeof(SingletonAttribute));
            RegisterWithAttribute(ref services, typeof(TransientAttribute));
            RegisterWithAttribute(ref services, typeof(ScopedAttribute));
        }

        private static void RegisterWithAttribute(ref IServiceCollection services, Type injectableAttribute)
        {
            var loadedAssemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location)
                .ToArray();

            var referencedPaths = Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths
                .Where(r => !loadedPaths
                    .Contains(r, StringComparer.InvariantCultureIgnoreCase))
                .ToList();
            
            toLoad.ForEach(path => loadedAssemblies
                    .Add(AppDomain.CurrentDomain
                        .Load(AssemblyName.GetAssemblyName(path))));

            foreach (var assembly in loadedAssemblies)
            {
                var allTypes = assembly.GetTypes();

                var classesWithAttribute = allTypes
                    .Where(t => t.IsClass && t.CustomAttributes
                        .Any(a => a.AttributeType == injectableAttribute));

                foreach (var implementationType in classesWithAttribute)
                {
                    var interfaceType = implementationType.GetInterfaces()
                        .FirstOrDefault(t => t.Name.Substring(1) == implementationType.Name);

                    if(interfaceType == null)
                        throw new Exception(
                            $"Failed to resolve interface for class {implementationType.Name}, unable to inject dependency!");

                    services = injectableAttribute switch
                    {
                        {} t when t == typeof(TransientAttribute) =>
                            services.AddTransient(interfaceType, implementationType),
                        {} t when t == typeof(ScopedAttribute) =>
                            services.AddScoped(interfaceType, implementationType),
                        {} t when t == typeof(SingletonAttribute) =>
                            services.AddSingleton(interfaceType, implementationType),
                        _ => services
                    };
                }
            }
        }
    }
}