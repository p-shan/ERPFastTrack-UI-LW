using ERPFastTrack.Abstraction.AbstractClass;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.DBGround.DBModels.Custom;
using ERPFastTrack.DBGround.DBModels.Identity;
using ERPFastTrack.LicenseProcessor.Internals;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace AspnetCoreStarter.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _orgRoleManagerAbstract;
        private readonly ILicenseAuth _licenseAuth;
        private readonly SignInManager<UserData> _signInManager;
        private readonly UserManager<UserData> _userManager;
        private readonly IUserStore<UserData> _userStore;
        private readonly IUserEmailStore<UserData> _emailStore;
        private readonly ILogger<IndexModel> _logger;
        private readonly IEmailSender _emailSender;

        public IndexModel(ERPFastTrackUIContext context, OrgRoleManagerAbstract orgRoleManagerAbstract, ILicenseAuth licenseAuth,
            UserManager<UserData> userManager,
            IUserStore<UserData> userStore,
            SignInManager<UserData> signInManager,
            ILogger<IndexModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _orgRoleManagerAbstract = orgRoleManagerAbstract;
            _licenseAuth = licenseAuth;

            RequestLicenseContent = _licenseAuth.GetDownloadableRequest();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        public string ReturnUrl { get; set; }

        public string RequestLicenseContent { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "LicenseCode")]
            public string LicenseCode { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGet()
        {
            var license = _context.Licenses.FirstOrDefault();
            if (license != null && license.IsValidLicense())
            {
                return RedirectToPage("/Dashboard/Index");
            }
            else if (license != null)
            {
                return RedirectToPage("/LicenseExpired");
            }
            ViewData["RequestLicenseContent"] = RequestLicenseContent;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _licenseAuth.Authenticate(Input.LicenseCode, _orgRoleManagerAbstract?.Role?.OrgName);

                    // Valid and Email Matching
                    bool successFlag = false;
                    if (res.IsValid && string.Equals(res.UserEmail, Input.Email, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Create User
                        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                        var user = CreateUser();
                        user.FullName = Input.Email;

                        await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                        await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                        var result = await _userManager.CreateAsync(user, Input.Password);

                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User created a new account with password.");

                            var userId = await _userManager.GetUserIdAsync(user);
                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var confirmUserRes = await _userManager.ConfirmEmailAsync(user, code);
                            if (confirmUserRes.Succeeded)
                            {
                                successFlag = true;

                                //var callbackUrl = Url.Page(
                                //    "/Account/ConfirmEmail",
                                //pageHandler: null,
                                //    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl, orgName = res.OrgName },
                                //    protocol: Request.Scheme);

                                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                                var org = await _context.Organizations.FirstOrDefaultAsync();
                                if (org == null)
                                {
                                    var adminRoleData = _context.OrgRoles.Where(x => x.RoleName == "ADMINISTRATOR").FirstOrDefault();
                                    Organization organization = new() { Name = res.OrgName, CreatedByUserId = userId };
                                    _context.Organizations.Add(organization);
                                    await _context.SaveChangesAsync();
                                    _context.OrgUsersRelationships.Add(new OrgUsersRelationship() { OrgId = organization.Id, UserId = user.Id, RoleId = adminRoleData.Id });

                                    await _context.SaveChangesAsync();

                                    HttpContext.Response.Cookies.Append("selectedOrg", organization.Id.ToString());
                                    HttpContext.Response.Cookies.Append("selectedOrgName", organization.Name.ToString());

                                    License license = new License();
                                    license.LicenseCode = Input.LicenseCode;
                                    license.OrgId = organization.Id;
                                    license.IsValid = true;
                                    license.LastValidation = DateTime.Now;
                                    _context.Licenses.Add(license);
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    var license = await _context.Licenses.FirstOrDefaultAsync();
                                    _context.Entry(license).State = EntityState.Modified;
                                    license.IsValid = true;
                                    license.LastValidation = DateTime.Now;
                                    license.LicenseCode = Input.LicenseCode;
                                    await _context.SaveChangesAsync();
                                }

                                return LocalRedirect(returnUrl);
                            }
                        }

                        if (!successFlag)
                        {
                            ModelState.AddModelError(string.Empty, "Administrator creation failed.");
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid license code.");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Validation failed.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Exception occurred, contact ERPFastTrack");
            }

            // If we got this far, something failed, redisplay form
            return Page();
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
