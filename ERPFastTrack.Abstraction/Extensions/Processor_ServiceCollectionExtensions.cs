using ERPFastTrack.Abstraction.Delegates;
using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.Abstraction.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ERPFastTrack.Abstraction.Extensions
{
    public static class Processor_ServiceCollectionExtensions
    {
        public static IServiceCollection AddProcessorFactory(this IServiceCollection services)
        {
            services.AddTransient<ProcessorFactory>(implementationFactory => key =>
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList().Where(x => x.FullName.Contains("ERPFastTrack"));

                var allTypes = new List<Type>();

                foreach (var assembly in loadedAssemblies)
                {
                    try
                    {
                        Type? targetType = assembly?.GetTypes().Where(x => x.Name == key).FirstOrDefault();
                        Type interfaceType = typeof(IProcessor);
                        bool implementsInterface = interfaceType.IsAssignableFrom(targetType);

                        if (targetType != null && implementsInterface)
                            return (IProcessor)implementationFactory.GetRequiredService(targetType);
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        // Handle the case where some types cannot be loaded
                        var loaderExceptions = ex.LoaderExceptions;
                        string err = string.Empty;
                        foreach (var loaderException in loaderExceptions)
                        {
                            err += "Error loading type: " + loaderException.Message + "\n";
                        }

                        throw new Exception(err, ex);
                    }
                }

                throw new NotImplementedException($"Processor - {key} not implemented");
            });

            return services;
        }
    }
}
