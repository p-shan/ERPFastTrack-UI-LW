using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.DBGround.DBModels.Identity;
using ERPFastTrack.Abstraction.AbstractClass;
using Microsoft.EntityFrameworkCore;
using ERPFastTrack.DBGround.DBModels.Custom;
using ERPFastTrack.API.Internals.Authorization;
using ERPFastTrack.API.Internals.Authorization.AuthTypes;
using ERPFastTrack.APIModels.OrgModels.Request;
using ERPFastTrack.APIModels.OrgModels.Response;
using Microsoft.AspNetCore.Authentication;
using ERPFastTrack.Common.Static;
using Microsoft.Extensions.Logging;

namespace ERPFastTrack.API.Internals.Controllers.Exposed
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly ILogger<OrganizationController> _logger;
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _orgRoleManager;
        private readonly UserManager<UserData> _userManager;
        private readonly IUserStore<UserData> _userStore;
        private readonly SignInManager<UserData> _signInManager;
        private readonly IUserEmailStore<UserData> _emailStore;

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public OrganizationController(ILogger<OrganizationController> logger,
            ERPFastTrackUIContext context,
            OrgRoleManagerAbstract orgRoleManager,
            UserManager<UserData> userManager,
            IUserStore<UserData> userStore,
            SignInManager<UserData> signInManager)
        {
            this._logger = logger;
            _context = context;
            _orgRoleManager = orgRoleManager;
            this._userManager = userManager;
            this._userStore = userStore;
            this._signInManager = signInManager;
            _emailStore = GetEmailStore();
        }

        // GET: api/Organization
        [HttpGet]
        public async Task<ActionResult<UserOrganizationsResult>> GetOrganizations()
        {
            UserOrganizationsResult userOrganizationsResult = new();
            if (_context.Organizations == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user.");
            }


            var result = (await _context.Organizations.ToListAsync()).Select(x =>
            {
                var res = new UserOrganization { Id = x.Id, OrganizationName = x.Name, OrgRole = user.Organizations.Where(y => y.OrgId == x.Id).First().Role.RoleName };
                return res;
            }
            ).ToList();

            return Ok(result);
        }

        // GET: api/Organization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(int id)
        {
            if (_context.Organizations == null)
            {
                return NotFound();
            }
            var organization = await _context.Organizations.FindAsync(id);

            if (organization == null)
            {
                return NotFound();
            }

            return organization;
        }
        [HttpPost("postusers")]
        [DynamicAnyAuthorize(typeof(AdminUser), typeof(SuperAdminUser))]
        public async Task<AddOrganizationUserResult> PostOrganizationUser(AddOrganizationUserRequest request)
        {
            AddOrganizationUserResult result = new();

            if (request.OrgRole < 1 && !string.IsNullOrWhiteSpace(request.UserPassword) && !string.IsNullOrWhiteSpace(request.EmailId) && !string.IsNullOrWhiteSpace(request.Name))
            {
                throw new Exception("Validation failed.");
            }

            try
            {
                if (_context.Organizations == null)
                {
                    throw new Exception("Entity set 'ERPFastTrackUIContext.Organizations' is null.");
                }
                bool successFlag = false;
                var roleData = _context.OrgRoles.Where(x => x.Id == request.OrgRole).FirstOrDefault();
                if (roleData != null)
                {
                    var user = _context.Users.FirstOrDefault(u => u.Email.ToUpper() == request.EmailId.ToUpper());
                    if (user != null)
                    {
                        if (user.Organizations != null)
                        {
                            foreach (var item in user.Organizations)
                            {
                                if (item.OrgId == _orgRoleManager.Role.OrgId)
                                    throw new Exception("User already has the access to the organization. Update the record if you want to change the user role.");
                            }
                        }

                        await _context.SaveChangesAsync();
                        _context.OrgUsersRelationships.Add(new OrgUsersRelationship() { OrgId = _orgRoleManager.Role.OrgId, UserId = user.Id, RoleId = roleData.Id });
                        await _context.SaveChangesAsync();
                        result.Status = true;
                    }
                    // Create new user
                    else
                    {
                        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                        var newUser = CreateUser();
                        newUser.FullName = request.Name;

                        await _userStore.SetUserNameAsync(newUser, request.EmailId, CancellationToken.None);
                        await _emailStore.SetEmailAsync(newUser, request.EmailId, CancellationToken.None);
                        var userCreateResult = await _userManager.CreateAsync(newUser, request.UserPassword);

                        if (userCreateResult.Succeeded)
                        {
                            _logger.LogInformation("User created a new account with password.");

                            var userId = await _userManager.GetUserIdAsync(newUser);
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                            var confirmUserRes = await _userManager.ConfirmEmailAsync(newUser, code);
                            if (confirmUserRes.Succeeded)
                            {
                                //var callbackUrl = Url.Page(
                                //    "/Account/ConfirmEmail",
                                //pageHandler: null,
                                //    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl, orgName = res.OrgName },
                                //    protocol: Request.Scheme);

                                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                                _context.OrgUsersRelationships.Add(new OrgUsersRelationship() { OrgId = _orgRoleManager.Role.OrgId, UserId = newUser.Id, RoleId = roleData.Id });

                                await _context.SaveChangesAsync();
                                successFlag = true;

                            }
                        }
                    }
                }

                if (successFlag)
                {
                    result.Status = true;
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [HttpGet("getusers")]
        [DynamicAnyAuthorize(typeof(AdminUser), typeof(SuperAdminUser))]
        public async Task<ActionResult<OrganizationUsersResult>> GetOrganizationUsers()
        {
            OrganizationUsersResult result = new();
            result.Users = new();

            if (_context.Organizations == null)
            {
                return NotFound();
            }

            if (Request.Cookies["selectedOrg"] == null)
            {
                return result;
            }

            var orgUsersRelationships = _context.OrgUsersRelationships.Where(x => x.OrgId == Convert.ToInt32(Request.Cookies["selectedOrg"]));

            if (orgUsersRelationships == null)
            {
                return NotFound();
            }

            result.Users = new();
            foreach (var item in orgUsersRelationships)
            {
                if (item.Role.RoleName == "SUPERADMINISTRATOR") continue;
                result.Users.Add(new OrganizationUser() { Email = item.UserData.Email, Name = item.UserData.FullName, Role = item.Role.RoleName });
            }

            return result;
        }
        private bool OrganizationExists(int id)
        {
            return (_context.Organizations?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private UserData CreateUser()
        {
            try
            {
                return Activator.CreateInstance<UserData>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(UserData)}'. " +
                    $"Ensure that '{nameof(UserData)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<UserData> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<UserData>)_userStore;
        }
    }
}
