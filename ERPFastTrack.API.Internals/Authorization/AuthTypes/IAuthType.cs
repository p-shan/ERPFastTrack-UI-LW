using Microsoft.AspNetCore.Mvc.Filters;

namespace ERPFastTrack.API.Internals.Authorization.AuthTypes
{
	public interface IAuthType
	{
		bool Authenticate(AuthorizationFilterContext context);
		string Message();
	}
}
