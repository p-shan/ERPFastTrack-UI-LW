using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.SourceProcessor.Sources.File;
using ERPFastTrack.SourceProcessor.Sources.SQLServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ERPFastTrack.SourceProcessor.Extensions
{
    public static class SourceProcessor_ServiceCollectionExtensions
    {
        public static IServiceCollection AddSourceProcessors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IOperationsSource<SQLServerSourceRequest>, SQLServerSource>();
            services.AddTransient<IOperationsSource<FileSourceRequest>, FileSource>();

            string targetNamespace = "ERPFastTrack.SourceProcessor.Implementations";
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
