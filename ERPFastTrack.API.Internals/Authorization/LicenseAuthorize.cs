using ERPFastTrack.DBGround.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ERPFastTrack.API.Internals.Authorization
{
    
    
    public class LicenseAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public LicenseAuthorizeAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var dbContext = context.HttpContext.RequestServices.GetService<ERPFastTrackUIContext>();

            var license = dbContext?.Licenses.FirstOrDefault();

            if (!(license != null && license.IsValidLicense()))
            {
                context.Result = new RedirectResult("/LicenseExpired");
            }

            return;
        }
    }
}
