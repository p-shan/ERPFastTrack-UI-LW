// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using ERPFastTrack.DBGround.DBModels.Identity;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.DBGround.DBModels.Custom;

namespace AspnetCoreStarter.Areas.Identity.Pages.Account
{
    //[System.Reflection.Obfuscation(Exclude = true)]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<UserData> _userManager;
        private readonly ERPFastTrackUIContext _context;

        public ConfirmEmailModel(UserManager<UserData> userManager, ERPFastTrackUIContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code, string orgName)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            if (orgName == null)
            {
                return NotFound($"Unable to confirm the user '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";

            try
            {
                if (_context.Organizations == null)
                {
                    throw new Exception("Entity set 'ERPFastTrackUIContext.Organizations' is null.");
                }

                Organization organization = new() { Name = orgName };
                var adminRoleData = _context.OrgRoles.Where(x => x.RoleName == "ADMINISTRATOR").FirstOrDefault();
                if (adminRoleData != null)
                {
                    _context.Organizations.Add(organization);
                    await _context.SaveChangesAsync();
                    _context.OrgUsersRelationships.Add(new OrgUsersRelationship() { OrgId = organization.Id, UserId = user.Id, RoleId = adminRoleData.Id });
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToPage("/Index");
            //return Page();
        }
    }
}
