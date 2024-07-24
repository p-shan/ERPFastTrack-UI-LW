using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ERPFastTrack.Common.Operations
{
	public static class Utils
	{
        public static string LOCAL_CLIENTID { get; set; } = "3MVG9Rr0EZ2YOVMaF_OQCb6aZnrsa3ljB2Yo_76ef4N5Wpg227inwkxjiBh0gzlHLIMAEQDmub.z8RZirMaZk";
        public static string LOCAL_CLIENTSECRET { get; set; } = "38306BAEF620F1A5E21D33D2E060443024AADD80E026F4F1D53C6DE6DCAE3452";
        public static string SVR_CLIENTID { get; set; } = "3MVG9gtDqcOkH4PIUtE_JWsIyi0rW9dmFkk2oU68plWSL.rfUKTEKaX2fK1iGEiyeBuQmRhd1UuL4fyMheYn6";
        public static string SVR_CLIENTSECRET { get; set; } = "3816F1B903BE17AB7B99622B437196BB016DA2E2BCC7C73BD82C79CD6A943A54";


        public static T GetService<T>(HttpContext context) where T : class
		{
			return context.RequestServices.GetRequiredService<T>();
		}

		public static void MapProperties(object source, object destination)
		{
			// Get the type information of both objects
			Type sourceType = source.GetType();
			Type destinationType = destination.GetType();

			// Iterate through each property in the source object
			foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
			{
				// Find a matching property in the destination object
				PropertyInfo destinationProperty = destinationType.GetProperty(sourceProperty.Name);

				// If a matching property is found and both properties have the same type
				if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType)
				{
					// Copy the value from the source property to the destination property
					object value = sourceProperty.GetValue(source);
					destinationProperty.SetValue(destination, value);
				}
			}
		}

        public static bool IsLocal()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        }

        public static void MapProperties<T,U>(object source, object destination)
        {
            // Get the type information of both objects
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            // Iterate through each property in the source object
            foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
            {
                // Find a matching property in the destination object
                PropertyInfo destinationProperty = destinationType.GetProperty(sourceProperty.Name);

                // If a matching property is found and both properties have the same type
                if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType)
                {
                    // Copy the value from the source property to the destination property
                    object value = sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, value);
                }
            }
        }
    }
}
