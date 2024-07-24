using ERPFastTrack.Abstraction.AbstractClass;
using ERPFastTrack.Common.Static;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.DBGround.DBModels.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ERPFastTrack.API.Internals.Middlewares
{
    
    
    public class RoleMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, OrgRoleManagerAbstract orgRoleManager, ERPFastTrackUIContext dbContext, UserManager<UserData> userManager)
        {
            // Check if request path starts with /wwwroot/
            if (context.Request.Path.StartsWithSegments("/css"))
            {
                // Skip processing and pass to the next middleware
                await _next(context);
                return;
            }

            // Check if request path starts with /wwwroot/
            if (context.Request.Path.StartsWithSegments("/img"))
            {
                // Skip processing and pass to the next middleware
                await _next(context);
                return;
            }

            // Check if request path starts with /wwwroot/
            if (context.Request.Path.StartsWithSegments("/js"))
            {
                // Skip processing and pass to the next middleware
                await _next(context);
                return;
            }

            // Check if request path starts with /wwwroot/
            if (context.Request.Path.StartsWithSegments("/json"))
            {
                // Skip processing and pass to the next middleware
                await _next(context);
                return;
            }

            // Check if request path starts with /wwwroot/
            if (context.Request.Path.StartsWithSegments("/vendor"))
            {
                // Skip processing and pass to the next middleware
                await _next(context);
                return;
            }

            // Access the ClaimsPrincipal from HttpContext
            ClaimsPrincipal User = context.User;

            if (User == null || !User.Identity.IsAuthenticated)
            {
                orgRoleManager.LoggedInUser = false;
                await _next(context);
            }
            else
            {
                orgRoleManager.LoggedInUser = true;
                orgRoleManager.Role = new();

                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    orgRoleManager.LoggedInUser = false;
                    await _next(context);
                }
                var org = dbContext.Organizations.FirstOrDefault();
                if (org != null)
                {

                    var cookie = context.Request.Cookies["selectedOrg"];
                    if (string.IsNullOrEmpty(cookie))
                    {
                        context.Response.Cookies.Append("selectedOrg", org.Id.ToString());
                    }

                    orgRoleManager.IsOrgSelected = true;
                    orgRoleManager.Role.OrgId = org.Id;
                    orgRoleManager.Role.OrgName = org.Name;

                    var userOrg = dbContext.OrgUsersRelationships.Where(x => x.OrgId == org.Id && x.UserId == user.Id).FirstOrDefault();
                    if (userOrg != null)
                    {
                        orgRoleManager.Role.UserId = userOrg.UserId;
                        var role = dbContext.OrgRoles.Where(x => x.Id == userOrg.RoleId).FirstOrDefault();
                        if (role != null)
                        {
                            orgRoleManager.Role.RoleName = role.RoleName;
                            orgRoleManager.Role.Role = (RolesEnum)role.Id;
                            orgRoleManager.Role.UserId = user.Id;
                            orgRoleManager.Role.UserEmail = user.Email;
                            orgRoleManager.Role.UserFullName = user.FullName;
                        }
                    }

                }
                else
                {
                    orgRoleManager.IsOrgSelected = false;
                }

                await _next(context);
            }

        }
    }
}
