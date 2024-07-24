using ERPFastTrack.Abstraction.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ERPFastTrack.SalesforceProcessor.Common;

namespace ERPFastTrack.SalesforceProcessor.Extensions
{
    public static class SalesforceProcessor_ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesforceProcessors(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SalesforceOptions>(configuration.GetSection("salesforceConfigurations"));
            services.AddHttpClient();

            string targetNamespace = "ERPFastTrack.SalesforceProcessor.Implementations";
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
