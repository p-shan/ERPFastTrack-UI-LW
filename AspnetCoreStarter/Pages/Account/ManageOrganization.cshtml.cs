using ERPFastTrack.API.Internals.Authorization;
using ERPFastTrack.DBGround.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetCoreStarter.Pages.Account
{
    [Authorize]
    [LicenseAuthorize]
    public class ManageOrgModel : PageModel
    {
        private readonly ERPFastTrackUIContext context;

        public ManageOrgModel(ERPFastTrackUIContext context)
        {
            this.context = context;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSubmit()
        {
            return Page();
        }
    }
}

