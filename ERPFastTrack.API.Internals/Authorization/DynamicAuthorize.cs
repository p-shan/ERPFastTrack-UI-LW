using ERPFastTrack.API.Internals.Authorization.AuthTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERPFastTrack.API.Internals.Authorization
{
    
    
	public class DynamicAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
	{
		public List<IAuthType> AuthTypes { get; set; }
		public DynamicAuthorizeAttribute(params Type[] types)
		{
			AuthTypes = new List<IAuthType>();
			foreach (var authType in types)
			{
				// Create an instance of MyClass using reflection
				AuthTypes.Add((IAuthType)Activator.CreateInstance(authType));
			}
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			foreach (var authType in AuthTypes)
			{
				if (!authType.Authenticate(context))
				{
					context.Result = new UnauthorizedResult();
				}
			}

			return;
		}
	}
}
