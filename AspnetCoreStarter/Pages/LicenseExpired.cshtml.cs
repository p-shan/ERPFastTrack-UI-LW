using ERPFastTrack.Abstraction.AbstractClass;
using ERPFastTrack.API.Internals.Authorization;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.LicenseProcessor.Internals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AspnetCoreStarter.Pages
{
    [Authorize]
    public class LicenseExpiredModel : PageModel
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly ILicenseAuth _licenseAuth;
        private readonly OrgRoleManagerAbstract _orgRoleManagerAbstract;

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        public string ReturnUrl { get; set; }
        public class InputModel
        {
            [Required]
            [Display(Name = "LicenseCode")]
            public string LicenseCode { get; set; }
        }
        public LicenseExpiredModel(ERPFastTrackUIContext context, ILicenseAuth licenseAuth, OrgRoleManagerAbstract orgRoleManagerAbstract)
        {
            this._context = context;
            this._licenseAuth = licenseAuth;
            _orgRoleManagerAbstract = orgRoleManagerAbstract;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _licenseAuth.Authenticate(Input.LicenseCode, _orgRoleManagerAbstract?.Role?.OrgName);

                    if (res.IsValid)
                    {
                        var license = await _context.Licenses.FirstOrDefaultAsync();
                        _context.Entry(license).State = EntityState.Modified;
                        license.IsValid = true;
                        license.LastValidation = DateTime.Now;
                        license.LicenseCode = Input.LicenseCode;
                        await _context.SaveChangesAsync();

                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid license code.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid license code.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong contact administrator.");
            }
            return Page();
        }

        public IActionResult OnGet()
        {
            var existsRes = _licenseAuth.LicenseExist();
            if (existsRes.Exists && existsRes.License.IsValidLicense())
            {
                return RedirectToPage("/Dashboard/Index");
            }

            return Page();
        }
    }
}
