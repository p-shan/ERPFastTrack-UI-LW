using ERPFastTrack.API.Internals.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetCoreStarter.Pages.Account
{
    [Authorize]
    [LicenseAuthorize]
    public class ManageOrgModel_Obselete : PageModel
    {

        public async Task OnGet()
        {

        }

    }
}
