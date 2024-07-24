using ERPFastTrack.Abstraction.AbstractClass;
using ERPFastTrack.Common.Static;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ERPFastTrack.API.Internals.Authorization.AuthTypes
{
	public class AdminUser : IAuthType
	{
		public string AuthMessage { get; set; }
		public bool Status { get; set; }

		public bool Authenticate(AuthorizationFilterContext context)
		{
			var roleManager = context.HttpContext.RequestServices.GetRequiredService<OrgRoleManagerAbstract>();
			if (roleManager.LoggedInUser && roleManager.IsOrgSelected && roleManager.Role.Role == RolesEnum.ADMINISTRATOR)
			{
				return true;
			}
			AuthMessage = "Not Authorized Administrator";
			return false;
		}

		public string Message()
		{
			return AuthMessage;
		}
	}
}
