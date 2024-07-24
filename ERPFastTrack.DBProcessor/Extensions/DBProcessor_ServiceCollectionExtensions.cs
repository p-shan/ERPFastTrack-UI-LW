using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.DBProcessor.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ERPFastTrack.DBProcessor.Extensions
{
    public static class DBProcessor_ServiceCollectionExtensions
    {
        public static IServiceCollection AddDBProcessors(this IServiceCollection services, IConfiguration configuration)
        {
            string targetNamespace = "ERPFastTrack.DBProcessor.Implementations";
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type interfaceType = typeof(IProcessor);
            var implementingTypes = assembly.GetTypes()
                .Where(type => type.Namespace != null && type.Namespace.StartsWith(targetNamespace))
                .Where(type => interfaceType.IsAssignableFrom(type) && !type.IsInterface);

            foreach (var type in implementingTypes)
                services.AddTransient(type);
            return services;
        }
    }
}
