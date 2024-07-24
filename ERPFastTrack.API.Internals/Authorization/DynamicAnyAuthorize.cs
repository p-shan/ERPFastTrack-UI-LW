using ERPFastTrack.API.Internals.Authorization.AuthTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace ERPFastTrack.API.Internals.Authorization
{
    
    
    public class DynamicAnyAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
	{
		public List<IAuthType> AuthTypes { get; set; }
		public DynamicAnyAuthorizeAttribute(params Type[] types)
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
			bool status = false;
			foreach (var authType in AuthTypes)
			{
				if (authType.Authenticate(context))
				{
					status = true;
					break;
				}
			}

			if (!status)
			{
				context.Result = new UnauthorizedResult();
			}

			return;
		}
	}
}
